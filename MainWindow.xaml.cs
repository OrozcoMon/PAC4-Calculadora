using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PAC4_Calculadora
{
    public partial class MainWindow : Window
    {
        private string entradaActual = "";
        private bool reiniciarPantalla = false;
        private bool errorMostrat = false;
        private bool puntDecimalAfegit = false;

        /// <summary>
        /// Constructor principal de la finestra. Inicialitza els components.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gestor d'esdeveniments per als botons numèrics.
        /// Afegeix el número seleccionat a l'entrada actual.
        /// </summary>
        private void Numero_Click(object sender, RoutedEventArgs e)
        {
            if (errorMostrat)
            {
                NetejarTot();
                errorMostrat = false;
            }

            if (reiniciarPantalla)
            {
                Pantalla.Text = "";
                entradaActual = "";
                reiniciarPantalla = false;
                puntDecimalAfegit = false;
            }

            var boto = (Button)sender;
            string numero = boto.Content.ToString();

            if (numero == "0" && (Pantalla.Text == "0" || string.IsNullOrEmpty(Pantalla.Text)))
            {
                return;
            }

            entradaActual += numero;
            Pantalla.Text += numero;
        }

        /// <summary>
        /// Gestor per al botó del punt decimal.
        /// Afegeix un punt decimal a l'entrada si no n'hi ha cap.
        /// </summary>
        private void PuntDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (errorMostrat)
            {
                NetejarTot();
                errorMostrat = false;
            }

            if (reiniciarPantalla)
            {
                Pantalla.Text = "";
                entradaActual = "0";
                reiniciarPantalla = false;
                puntDecimalAfegit = false;
            }

            if (!puntDecimalAfegit)
            {
                if (string.IsNullOrEmpty(entradaActual) || EsOperador(entradaActual[entradaActual.Length - 1]))
                {
                    entradaActual += "0";
                    Pantalla.Text += "0";
                }

                entradaActual += ".";
                Pantalla.Text += ".";
                puntDecimalAfegit = true;
            }
        }

        /// <summary>
        /// Gestor per als operadors (+, -, *, /).
        /// Afegeix l'operador a l'expressió si és vàlid.
        /// </summary>
        private void Operador_Click(object sender, RoutedEventArgs e)
        {
            if (errorMostrat)
            {
                NetejarTot();
                errorMostrat = false;
                return;
            }

            var boto = (Button)sender;
            string nouOperador = boto.Content.ToString();

            if (string.IsNullOrEmpty(entradaActual) && nouOperador != "-")
            {
                return;
            }

            if (Pantalla.Text.Length > 0 && EsOperador(Pantalla.Text[Pantalla.Text.Length - 1]))
            {
                Pantalla.Text = Pantalla.Text.Substring(0, Pantalla.Text.Length - 1);
                entradaActual = entradaActual.Substring(0, entradaActual.Length - 1);
            }

            entradaActual += nouOperador;
            Pantalla.Text += nouOperador;
            reiniciarPantalla = false;
            puntDecimalAfegit = false;
        }

        /// <summary>
        /// Gestor per al botó d'igual (=).
        /// Avalua l'expressió aritmètica i mostra el resultat.
        /// Mostra un missatge d'error si l'expressió és incorrecta.
        /// </summary>
        private void Igual_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(entradaActual) || errorMostrat)
            {
                return;
            }

            try
            {
                string expressioNet = entradaActual.Replace(",", ".");

                if (expressioNet.Length > 0 && EsOperador(expressioNet[expressioNet.Length - 1]))
                {
                    throw new SyntaxErrorException("L'expressió acaba amb operador");
                }

                if (TéOperadorsConsecutius(expressioNet))
                {
                    throw new SyntaxErrorException("Operadors consecutius no permesos");
                }

                if (Regex.IsMatch(expressioNet, @"([^\.0-9]|^)0([^\.0-9]|$)"))
                {
                    throw new DivideByZeroException();
                }

                double resultat = AvaluaExpressio(expressioNet);

                Pantalla.Text = FormatResultat(resultat);
                entradaActual = Pantalla.Text.Replace(",", ".");
                reiniciarPantalla = true;
                puntDecimalAfegit = Pantalla.Text.Contains(",");
            }
            catch (DivideByZeroException)
            {
                MostrarError();
            }
            catch (SyntaxErrorException)
            {
                MostrarError();
            }
            catch (Exception ex) when (ex is EvaluateException || ex is OverflowException)
            {
                MostrarError();
            }
        }

        /// <summary>
        /// Comprova si l'expressió conté operadors consecutius.
        /// </summary>
        /// <param name="expressio">Expressió aritmètica</param>
        /// <returns>True si hi ha operadors consecutius, sinó false</returns>
        private bool TéOperadorsConsecutius(string expressio)
        {
            for (int i = 1; i < expressio.Length; i++)
            {
                if (EsOperador(expressio[i]) && EsOperador(expressio[i - 1]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Avalua una expressió aritmètica i retorna el resultat.
        /// Llença excepció si hi ha divisió per zero.
        /// </summary>
        /// <param name="expressio">Expressió aritmètica</param>
        /// <returns>Resultat de l'expressió</returns>
        private double AvaluaExpressio(string expressio)
        {
            var dataTable = new DataTable();
            var result = dataTable.Compute(expressio, null);

            if (result.ToString() == "∞" || double.IsInfinity(Convert.ToDouble(result)))
            {
                throw new DivideByZeroException();
            }

            return Convert.ToDouble(result);
        }

        /// <summary>
        /// Dona format al resultat per mostrar-lo.
        /// Elimina zeros innecessaris i punts decimals sobrants.
        /// </summary>
        /// <param name="resultat">Resultat numèric</param>
        /// <returns>Cadena formatejada</returns>
        private string FormatResultat(double resultat)
        {
            if (resultat == Math.Truncate(resultat))
            {
                return resultat.ToString("0", CultureInfo.CurrentCulture);
            }
            else
            {
                string formatted = resultat.ToString("0.##########", CultureInfo.CurrentCulture);
                if (formatted.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                {
                    formatted = formatted.TrimEnd('0');
                    if (formatted.EndsWith(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                    {
                        formatted = formatted.Substring(0, formatted.Length - 1);
                    }
                }
                return formatted;
            }
        }

        /// <summary>
        /// Mostra un missatge d'error a la pantalla i reinicia l'estat de la calculadora.
        /// </summary>
        private void MostrarError()
        {
            Pantalla.Text = "Error";
            entradaActual = "";
            reiniciarPantalla = true;
            errorMostrat = true;
            puntDecimalAfegit = false;
        }

        /// <summary>
        /// Gestor per al botó de neteja.
        /// Neteja tota la pantalla i estat intern.
        /// </summary>
        private void Neteja_Click(object sender, RoutedEventArgs e)
        {
            NetejarTot();
        }

        /// <summary>
        /// Neteja tots els camps i reinicia l'estat de la calculadora.
        /// </summary>
        private void NetejarTot()
        {
            Pantalla.Text = "";
            entradaActual = "";
            reiniciarPantalla = false;
            errorMostrat = false;
            puntDecimalAfegit = false;
        }

        /// <summary>
        /// Comprova si un caràcter és un operador aritmètic (+, -, *, /).
        /// </summary>
        /// <param name="c">Caràcter a comprovar</param>
        /// <returns>True si és operador, sinó false</returns>
        private bool EsOperador(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
    }
}
