# GameBar

## Projekt leírása
A **GameBar** egy WPF alkalmazás, amely lehetőséget biztosít a felhasználók számára, hogy játékklienseket kezeljenek egy helyen. Az alkalmazás automatikusan felismeri a telepített játékplatformokat (pl. Steam, Epic Games, Xbox App, GOG), és lehetővé teszi azok elindítását.

## Fő funkciók
- Játékkliensek felismerése a meghajtókon.
- Ikonok megjelenítése a talált kliensekhez.
- Az elérhető kliensek indítása az alkalmazáson keresztül.
- Sötét mód (Dark Mode) támogatása.
- Modern WPF felhasználói felület.

## Telepítés
### Előfeltételek
A projekt futtatásához a következők szükségesek:
- **.NET 6 vagy újabb** (telepíthető: [Microsoft .NET](https://dotnet.microsoft.com/download))
- **Visual Studio 2022** (ajánlott) WPF fejlesztési környezettel

### Futtatás
1. Klónozd a repót:
   ```sh
   git clone https://github.com/felhasznalo/GameBar.git
   ```
2. Nyisd meg a **GameBar.sln** fájlt Visual Studio-ban.
3. Állítsd be a **GameBar** projektet fő projektnek.
4. Kattints a **Start** gombra vagy futtasd a következő parancsot:
   ```sh
   dotnet run
   ```

## Használt technológiák
- **C#**, **WPF** - Felhasználói felület fejlesztéshez
- **MVVM** minta alkalmazása
- **.NET 6+** a fő keretrendszer

## Járulj hozzá!
Ha szeretnél hozzájárulni a fejlesztéshez:
1. Forkold a repót!
2. Hozz létre egy új branch-et: `git checkout -b feature/uj-funkcio`
3. Végezd el a módosításokat, majd commitolj: `git commit -m "Hozzáadva uj funkcio"`
4. Pushold fel a branch-et: `git push origin feature/uj-funkcio`
5. Nyiss egy Pull Requestet!

## Licenc
Ez a projekt az **MIT licenc** alatt van kiadva. Részletek a [LICENSE](LICENSE) fájlban.

---
# GameBar

## Project Description
**GameBar** is a WPF application that allows users to manage game clients in one place. The application automatically detects installed game platforms (e.g., Steam, Epic Games, Xbox App, GOG) and enables users to launch them from a unified interface.

## Key Features
- Detects installed game clients on the system.
- Displays icons for recognized clients.
- Allows launching available clients directly from the app.
- Supports Dark Mode.
- Modern WPF user interface.

## Installation
### Prerequisites
To run this project, you need:
- **.NET 6 or later** (Download from [Microsoft .NET](https://dotnet.microsoft.com/download))
- **Visual Studio 2022** (recommended) with WPF development environment

### Running the Application
1. Clone the repository:
   ```sh
   git clone https://github.com/username/GameBar.git
   ```
2. Open **GameBar.sln** in Visual Studio.
3. Set **GameBar** as the startup project.
4. Click the **Start** button or run the following command:
   ```sh
   dotnet run
   ```

## Technologies Used
- **C#**, **WPF** - For user interface development
- **MVVM** architectural pattern
- **.NET 6+** as the main framework

## Contributing
If you want to contribute:
1. Fork the repository.
2. Create a new branch: `git checkout -b feature/new-feature`
3. Make your changes and commit: `git commit -m "Added new feature"`
4. Push the branch: `git push origin feature/new-feature`
5. Open a Pull Request!

## License
This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---
