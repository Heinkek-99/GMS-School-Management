# 🎓 GMS - Gestion Management School

[![Build Status](https://github.com/Heinkek-99/GMS-School-Management/workflows/CI%2FCD/badge.svg)](https://github.com/Heinkek-99/GMS-School-Management/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yel1low.svg)](https://opensource.org/licenses/MIT)
[![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](http://makeapullrequest.com)
[![codecov](https://codecov.io/gh/Heinkek-99/GMS-School-Management/branch/main/graph/badge.svg)](https://codecov.io/gh/Heinkek-99/GMS-School-Management)
[![GitHub issues](https://img.shields.io/github/issues/Heinkek-99/GMS-School-Management)](https://github.com/Heinkek-99/GMS-School-Management/issues)
[![GitHub stars](https://img.shields.io/github/stars/Heinkek-99/GMS-School-Management)](https://github.com/Heinkek-99/GMS-School-Management/stargazers)

> **Application Desktop Windows** pour la gestion complète des établissements scolaires (écoles primaires, collèges, lycées) en Afrique francophone.

---

## 📋 Table des matières

- [🎯 À propos](#-à-propos)
- [✨ Fonctionnalités](#-fonctionnalités)
- [🖥️ Captures d'écran](#️-captures-décran)
- [🚀 Démarrage rapide](#-démarrage-rapide)
- [🛠️ Technologies](#️-technologies)
- [📦 Installation](#-installation)
- [🏗️ Architecture](#️-architecture)
- [🧪 Tests](#-tests)
- [📚 Documentation](#-documentation)
- [🤝 Contribution](#-contribution)
- [📄 Licence](#-licence)
- [👥 Auteurs](#-auteurs)

---

## 🎯 À propos

**GMS (Gestion Management School)** résout le problème majeur des établissements scolaires : **le suivi financier des familles ayant plusieurs enfants inscrits**.

### Problème résolu

❌ **AVANT GMS :**
- ⏱️ 45 minutes pour inscrire un élève
- 📊 Confusion dans les paiements multi-enfants
- 📝 Données éparpillées (Excel, cahiers)
- 💸 Taux d'impayés élevé (absence de suivi)
- 🔍 Impossibilité de vision globale

✅ **AVEC GMS :**
- ⚡ 8 minutes pour inscrire un élève
- 👨‍👩‍👧‍👦 Vision consolidée par famille
- 💾 Données centralisées et sécurisées
- 📈 +25% de taux de recouvrement
- 📊 Dashboard temps réel

### Cibles

- 🎯 **Établissements privés** : 50-500 élèves
- 🌍 **Géographie** : Afrique francophone (Sénégal, Côte d'Ivoire, Bénin, etc.)
- 💼 **Utilisateurs** : Directeurs, Comptables, Secrétaires

---

## ✨ Fonctionnalités

### 🔐 Authentification & Sécurité
- ✅ Login sécurisé (BCrypt)
- ✅ 4 rôles : Admin, Directeur, Secrétaire, Comptable
- ✅ Permissions granulaires
- ✅ Audit trail complet

### 👨‍👩‍👧‍👦 Gestion des Familles
- ✅ Fiche famille complète (parents, contacts, adresse)
- ✅ **Vision consolidée** : Total dû, Total payé, Solde global
- ✅ Liste de tous les enfants
- ✅ Historique des paiements familiaux

### 🎓 Gestion des Élèves
- ✅ Inscription rapide avec photo
- ✅ Génération automatique du matricule (EL202400123)
- ✅ Dossier scolaire complet (onglets : Info, Finances, Notes, Documents)
- ✅ Recherche et filtres avancés

### 💰 Module Financier
- ✅ Configuration types de frais (scolarité, cantine, transport, etc.)
- ✅ Génération automatique des frais à l'inscription
- ✅ **Enregistrement paiement avec ventilation multi-enfants**
- ✅ Bulletin financier par élève
- ✅ Dashboard financier (KPIs, impayés, recouvrement)
- ✅ Génération reçus PDF

### 📄 Génération de Documents
- ✅ Carte d'élève (avec photo et QR code)
- ✅ Reçu de paiement
- ✅ Bulletin financier
- ✅ Certificat de scolarité
- ✅ Bulletin de notes

### 📊 Tableau de bord
- ✅ KPIs temps réel (élèves, encaissements, impayés)
- ✅ Top 10 familles impayées
- ✅ Graphiques évolution financière
- ✅ Export Excel

### 📚 Module Académique (Basique)
- ✅ Saisie notes par matière et période
- ✅ Calcul automatique des moyennes
- ✅ Bulletin de notes
- ✅ Emploi du temps par classe

---

## 🖥️ Captures d'écran

### Écran de connexion
![Login](docs/screenshots/login.png)

### Dashboard Directeur
![Dashboard](docs/screenshots/dashboard.png)

### Enregistrement Paiement (Ventilation)
![Paiement](docs/screenshots/paiement.png)

### Dossier Élève
![Dossier](docs/screenshots/dossier-eleve.png)

---

## 🚀 Démarrage rapide

### Prérequis

- **Windows** : 7 SP1, 8, 10, 11
- **RAM** : 4 GB minimum (8 GB recommandé)
- **Espace disque** : 2 GB
- **.NET 8 Runtime** (inclus dans l'installeur)
- **SQL Server Express** (inclus dans l'installeur)

### Installation (Pour Utilisateurs)

```bash
# 1. Télécharger l'installeur
https://github.com/Heinkek-99/GMS-School-Management/releases/latest

# 2. Exécuter GMS-Setup.msi
# Double-cliquer et suivre l'assistant

# 3. Lancer l'application
# Icône créée sur le bureau

# 4. Connexion par défaut
Username: admin
Password: Admin@123
```

### Installation (Pour Développeurs)

```bash
# 1. Cloner le repository
git clone https://github.com/Heinkek-99/GMS-School-Management.git
cd GMS-School-Management

# 2. Restaurer les packages
dotnet restore

# 3. Créer la base de données
dotnet ef database update --project src/GMS.Infrastructure --startup-project src/GMS.Desktop

# 4. Lancer l'application
dotnet run --project src/GMS.Desktop/GMS.Desktop.csproj

# OU ouvrir dans Visual Studio
start GMS.sln
```

### Identifiants par défaut

| Rôle | Username | Password |
|------|----------|----------|
| Admin | `admin` | `Admin@123` |
| Directeur | `directeur` | `Dir@123` |
| Secrétaire | `secretaire` | `Sec@123` |
| Comptable | `comptable` | `Compta@123` |

⚠️ **Changez ces mots de passe en production !**

---

## 🛠️ Technologies

### Backend

| Technologie | Version | Utilité |
|-------------|---------|---------|
| [.NET](https://dotnet.microsoft.com/) | 8.0 | Framework principal |
| [C#](https://docs.microsoft.com/dotnet/csharp/) | 12.0 | Langage |
| [Entity Framework Core](https://docs.microsoft.com/ef/core/) | 8.0 | ORM |
| [SQL Server Express](https://www.microsoft.com/sql-server) | 2022 | Base de données |
| [MediatR](https://github.com/jbogard/MediatR) | 12.2 | CQRS |
| [FluentValidation](https://fluentvalidation.net/) | 11.9 | Validation |
| [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net) | 4.0 | Hash passwords |

### Frontend

| Technologie | Version | Utilité |
|-------------|---------|---------|
| [WPF](https://docs.microsoft.com/dotnet/desktop/wpf/) | .NET 8 | Interface graphique |
| [MVVM Pattern](https://docs.microsoft.com/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern) | - | Architecture UI |
| [Material Design](https://material.io/) | - | Design system |

### Documents & Reports

| Technologie | Version | Utilité |
|-------------|---------|---------|
| [QuestPDF](https://www.questpdf.com/) | 2023.12 | Génération PDF |
| [QRCoder](https://github.com/codebude/QRCoder) | 1.4.3 | QR codes |
| [ClosedXML](https://github.com/ClosedXML/ClosedXML) | 0.102 | Export Excel |

### DevOps

| Technologie | Version | Utilité |
|-------------|---------|---------|
| [Git](https://git-scm.com/) | - | Version control |
| [GitHub Actions](https://github.com/features/actions) | - | CI/CD |
| [Docker](https://www.docker.com/) | - | Conteneurisation |
| [xUnit](https://xunit.net/) | 2.6 | Tests unitaires |
| [WiX Toolset](https://wixtoolset.org/) | 3.11 | Packaging MSI |

---

## 📦 Installation Développeur (Détaillée)

### 1. Installer les prérequis

```powershell
# Git
winget install Git.Git

# Visual Studio 2022 Community
winget install Microsoft.VisualStudio.2022.Community

# .NET 8 SDK
winget install Microsoft.DotNet.SDK.8

# SQL Server Express
winget install Microsoft.SQLServer.2022.Express

# Docker Desktop (Optionnel)
winget install Docker.DockerDesktop
```

### 2. Cloner et configurer

```bash
# Cloner
git clone https://github.com/Heinkek-99/GMS-School-Management.git
cd GMS-School-Management

# Installer outils EF Core
dotnet tool install --global dotnet-ef

# Restaurer packages
dotnet restore

# Build
dotnet build
```

### 3. Configurer la base de données

```bash
# Créer la base de données
dotnet ef database update --project src/GMS.Infrastructure --startup-project src/GMS.Desktop

# Vérifier la création
sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT name FROM sys.databases WHERE name = 'GmsDb'"
```

### 4. Lancer l'application

**Option A : Visual Studio**
```bash
# Ouvrir la solution
start GMS.sln

# Appuyer F5 pour déboguer
```

**Option B : Ligne de commande**
```bash
dotnet run --project src/GMS.Desktop/GMS.Desktop.csproj
```

**Option C : Docker**
```bash
docker-compose up -d
```

---

## 🏗️ Architecture

### Clean Architecture + CQRS

```
┌─────────────────────────────────────────────┐
│  Presentation Layer (WPF)                   │
│  - Views (XAML)                             │
│  - ViewModels (MVVM)                        │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│  Application Layer                          │
│  - Commands/Queries (CQRS)                  │
│  - Handlers (MediatR)                       │
│  - DTOs                                     │
│  - Validators (FluentValidation)            │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│  Domain Layer                               │
│  - Entities (Famille, Eleve, Paiement...)   │
│  - Value Objects                            │
│  - Business Rules                           │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│  Infrastructure Layer                       │
│  - DbContext (EF Core)                      │
│  - Repositories                             │
│  - External Services                        │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│  Database (SQL Server Express)              │
└─────────────────────────────────────────────┘
```

### Structure des dossiers

```
GMS-School-Management/
├── .github/
│   └── workflows/          # CI/CD
├── docs/                   # Documentation
├── scripts/                # Scripts DevOps
├── src/
│   ├── GMS.Domain/         # Entités métier
│   ├── GMS.Application/    # Logique métier (CQRS)
│   ├── GMS.Infrastructure/ # EF Core, Data Access
│   ├── GMS.Desktop/        # Application WPF
│   ├── GMS.Shared/         # DTOs, Constants
│   └── GMS.Tests/          # Tests
├── .gitignore
├── docker-compose.yml
├── GMS.sln
└── README.md
```

---

## 🧪 Tests

### Exécuter les tests

```bash
# Tous les tests
dotnet test

# Avec couverture de code
dotnet test --collect:"XPlat Code Coverage"

# Tests d'un projet spécifique
dotnet test src/GMS.Tests/GMS.Tests.csproj
```

### Couverture de code

![Coverage](https://codecov.io/gh/Heinkek-99/GMS-School-Management/branch/main/graphs/sunburst.svg)

**Objectif :** > 70% de couverture

### Types de tests

- ✅ **Tests unitaires** : Logique métier (Handlers, Services)
- ✅ **Tests d'intégration** : Base de données
- ✅ **Tests UI** : Manuels (checklist)

---

## 📚 Documentation

### Pour Utilisateurs

- 📖 [Manuel Utilisateur](docs/user-manual.pdf) (PDF, 60 pages)
- 🎥 [Vidéos Tutoriels](docs/videos/) (5 vidéos de 5 min)
- ❓ [FAQ](docs/faq.md)
- 📋 [Guide Installation](docs/installation.md)

### Pour Développeurs

- 🏗️ [Architecture Détaillée](docs/architecture.md)
- 📊 [Schéma Base de Données](docs/database-schema.png)
- 🔌 [Documentation API](docs/api-documentation.md)
- 🤝 [Guide de Contribution](CONTRIBUTING.md)
- 📜 [Changelog](CHANGELOG.md)

---

## 🤝 Contribution

Les contributions sont les bienvenues ! 🎉

### Comment contribuer ?

1. **Fork** le projet
2. **Créer** une branche feature (`git checkout -b feature/AmazingFeature`)
3. **Commit** vos changements (`git commit -m 'feat: Add amazing feature'`)
4. **Push** vers la branche (`git push origin feature/AmazingFeature`)
5. **Ouvrir** une Pull Request

📖 Lisez CONTRIBUTING.md[contributing.md] pour les détails complets.

### Conventions de commit

Nous suivons [Conventional Commits](https://www.conventionalcommits.org/) :

```
feat: Nouvelle fonctionnalité
fix: Correction de bug
docs: Documentation
style: Formatage
refactor: Refactorisation
test: Tests
chore: Maintenance
```

### Code de conduite

Veuillez lire [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) pour plus de détails.

---

## 📄 Licence

Ce projet est sous licence **MIT** - voir [LICENSE](LICENSE) pour plus de détails.

---

## 👥 Auteurs

### 👨‍💻 Lead Developer
**Heinkek** - [@Heinkek-99](https://github.com/Heinkek-99)

### Contributeurs

Merci à tous nos contributeurs ! 💖

<a href="https://github.com/Heinkek-99/GMS-School-Management/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=Heinkek-99/GMS-School-Management" />
</a>

---

## 🙏 Remerciements

- [Claude AI](https://claude.ai) - Assistance développement
- [Material Design](https://material.io/) - Design system
- [QuestPDF](https://www.questpdf.com/) - Génération PDF
- [.NET Foundation](https://dotnetfoundation.org/) - Framework

---

## 📞 Support & Contact

- 📧 **Email** : support@gms-school.com
- 💬 **Discord** : [Rejoindre le serveur](https://discord.gg/gms)
- 🐛 **Bugs** : [Créer une issue](https://github.com/Heinkek-99/GMS-School-Management/issues)
- 📖 **Documentation** : [Wiki](https://github.com/Heinkek-99/GMS-School-Management/wiki)

---

## 📈 Statistiques

![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Heinkek-99/GMS-School-Management)
![GitHub last commit](https://img.shields.io/github/last-commit/Heinkek-99/GMS-School-Management)
![GitHub repo size](https://img.shields.io/github/repo-size/Heinkek-99/GMS-School-Management)
![Lines of code](https://img.shields.io/tokei/lines/github/Heinkek-99/GMS-School-Management)

---

## 🗺️ Roadmap

### Version 1.0 (MVP) - ✅ Terminé
- [x] Gestion familles et élèves
- [x] Module financier complet
- [x] Génération documents PDF
- [x] Dashboard reporting

### Version 1.1 - 🚧 En cours
- [ ] Notifications SMS/Email
- [ ] Import/Export Excel massif
- [ ] Module présence/absences
- [ ] Statistiques avancées

### Version 2.0 - 📅 Planifié
- [ ] API REST
- [ ] Portail parents web
- [ ] Application mobile
- [ ] Intégration Mobile Money

---

<div align="center">

**Fait avec ❤️ pour les établissements scolaires africains**

[![Star on GitHub](https://img.shields.io/github/stars/Heinkek-99/GMS-School-Management.svg?style=social)](https://github.com/Heinkek-99/GMS-School-Management/stargazers)

[⬆ Retour en haut](#-gms---gestion-management-school)

</div>