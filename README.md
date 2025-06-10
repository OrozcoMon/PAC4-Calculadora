# <i class="fa-solid fa-calculator"></i> Calculadora WPF

Una calculadora bàsica desenvolupada amb **Windows Presentation Foundation (WPF)** per al mòdul _M0488 - Desenvolupament d’Interfícies_.

---

## <i class="fas fa-bookmark"></i> Taula de continguts
- [ Calculadora WPF](#-calculadora-wpf)
  - [ Taula de continguts](#-taula-de-continguts)
  - [ Requisits](#-requisits)
  - [ Característiques](#-característiques)
    - [⚖️ Prioritat d’operadors](#️-prioritat-doperadors)
    - [➕ Operacions bàsiques](#-operacions-bàsiques)
    - [✨ Funcionalitats addicionals](#-funcionalitats-addicionals)
  - [ Ús](#-ús)
  - [ Exemples](#-exemples)
  - [ Context acadèmic](#-context-acadèmic)
  - [ Llicència](#-llicència)

---

## <i class="fa-solid fa-plug"></i> Requisits

- <i class="fab fa-windows"></i> **Windows 10/11**
- <i class="fas fa-code"></i> **.NET 8.0 Runtime**
- <i class="fas fa-laptop-code"></i> **Visual Studio 2022** (per al desenvolupament)

---

## <i class="fa-solid fa-list-check"></i> Característiques

### ⚖️ Prioritat d’operadors
- Avalua `*` i `/` abans de `+` i `-`  
  _Exemple: `5 + 3 * 2 = 11`_

### ➕ Operacions bàsiques
- <i class="fas fa-plus"></i> **Suma**
- <i class="fas fa-minus"></i> **Resta**
- <i class="fas fa-times"></i> **Multiplicació**
- <i class="fas fa-divide"></i> **Divisió**

### ✨ Funcionalitats addicionals
- <i class="fas fa-undo"></i> **Neteja** (`C`)
- <i class="fas fa-exclamation-triangle"></i> **Gestió d’errors**
- <i class="fas fa-link"></i> **Operacions encadenades**

---

## <i class="fas fa-book-open"></i> Ús

1. **Operacions bàsiques**:
   - Fes clic als botons numèrics (0–9)
   - Selecciona l’operador (+, -, *, /)
   - Prem `=` per veure el resultat

2. **Reinici de càlcul**:
   - Prem `C` per netejar la pantalla

3. **Operacions encadenades**:
   - Exemple: `5 + 3 * 2` s’avalua correctament seguint la prioritat

4. **Gestió d’errors**:
   - Entrades incorrectes mostren `Error` a la pantalla

---

## <i class="fas fa-images"></i> Exemples


**_Input: 5 + 3_**
**_Resultat: 8_**
![Exemple de suma 1](screenshots/Suma_1.png) ![Exemple de suma 2](screenshots/Suma_2.png)


![Exemple d’operació encadenada](screenshots/chained.png)  
_Exemple: 5 + 3 * 2 = 11_

---

## <i class="fa-solid fa-graduation-cap"></i> Context acadèmic

Aquest projecte es va desenvolupar per posar en pràctica:

- <i class="fas fa-laptop-code"></i> Maquetació amb **WPF/XAML**
- <i class="fas fa-project-diagram"></i> Arquitectura d’esdeveniments
- <i class="fas fa-palette"></i> Principis de **UI/UX**
- <i class="fas fa-cogs"></i> Lògica funcional d’una calculadora

---

## <i class="fas fa-balance-scale"></i> Llicència

- **MIT License**  
- **Autora**: Montse Orozco

---

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
