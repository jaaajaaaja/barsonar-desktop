![Barsonar Desktop](./barsonar-desktop/images/barsonar_desktop.png)

## <span style="color:purple">Tartalomjegyzék</span>
- [Tartalomjegyzék](#tartalomjegyzék)
- [Stack](#stack)
- [Az applikáció célja](#az-applikáció-célja)
- [Előfeltételek](#előfeltételek)
- [Telepítés](#telepítés)
    - [1. Klónozás és függőségek telepítése](#1-klónozás-és-függőségek-telepítése)
    - [2. Projekt megnyitása](#2-projekt-megnyitása)
- [Futtatás](#futtatás)
- [Projekt felépítése](#projekt-felépítése)
- [Biztonsági megjegyzések](#biztonsági-megjegyzések)
    - [Fontos figyelmeztetések](#fontos-figyelmeztetések)
- [Hozzájárulás](#hozzájárulás)

---

## <span style="color:purple">Stack</span>

![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-512BD4?logo=windows&logoColor=white)
![XAML](https://img.shields.io/badge/XAML-512BD4)
![Windows](https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=white)

---

## <span style="color:purple">Az applikáció célja</span>

Ez a C# WPF asztali alkalmazás a BarSonar webes alkalmazás admin felületét valósítja meg.

Az alkalmazás a BarSonar backend végpontjatit használja, tehát ahhoz hogy az applikációt futtasd először szükséged lesz a [backendre](https://github.com/jaaajaaaja/VizsgaRemek_Backend).

A backendről részletes leírást a GitHub repository-ban találsz.

---

## <span style="color:purple">Előfeltételek</span>

A projekt futtatásához szükséges:

- **<span style="color:red">.NET SDK 10.0.102 vagy újabb</span>** (https://dotnet.microsoft.com/download)
- **<span style="color:red">Visual Studio 2022</span>** vagy **Visual Studio Code**
- **<span style="color:red">Windows 10 vagy újabb</span>**

---

## <span style="color:purple">Telepítés</span>

#### 1. Klónozás és függőségek telepítése

```bash
# Klónozd a repository-t  
git clone <repository-url>
```

#### 2. Projekt megnyitása

**Visual Studio 2022-vel:**
```bash
# Nyisd meg a .slnx fájlt
barsonar-desktop.slnx
```

---

## <span style="color:purple">Futtatás</span>

**Visual Studio 2022-vel:**
1. Miután megnyitottad Visual Studioban nyomd meg az `F5`-öt vagy kattints a "Start" gombra
2. Az alkalmazás ablaka megnyílik

**Parancssorból (CMD/PowerShell):**
```bash
# Futtasd a projektet
dotnet run --project barsonar-desktop/barsonar-desktop.csproj
```

---

## <span style="color:purple">Hozzájárulás</span>

A projekt fejlesztéséhez:

1. Hozz létre egy új ágat (`git checkout -b feature/uj-funkcio`)
2. Commitold a módosításokat (`git commit -m 'Új funkció hozzáadása'`)
3. Push-old az ágat (`git push origin feature/uj-funkcio`)
4. Nyiss egy Pull Request-et

---

**Szerző**: Barsonar Desktop csapat  
**Verzió**: 1.0.0
