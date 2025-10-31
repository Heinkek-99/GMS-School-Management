# Changelog

Toutes les modifications notables de ce projet seront documentées dans ce fichier.

Le format est basé sur [Keep a Changelog](https://keepachangelog.com/fr/1.0.0/),
et ce projet adhère à [Semantic Versioning](https://semver.org/lang/fr/).

## [Unreleased]

### À venir
- Notifications SMS/Email automatiques
- Import/Export Excel massif
- Module présence/absences
- Statistiques avancées

---

## [1.0.0] - 2025-01-22

### 🎉 Version initiale (MVP)

#### ✨ Ajouté
- **Authentification & Sécurité**
  - Login sécurisé avec BCrypt
  - 4 rôles : Admin, Directeur, Secrétaire, Comptable
  - Matrice de permissions granulaires
  - Audit trail complet

- **Gestion des Familles**
  - CRUD complet familles
  - Vision consolidée (Total dû, Payé, Solde)
  - Recherche et filtres
  - Liste enfants par famille

- **Gestion des Élèves**
  - Inscription rapide avec formulaire wizard
  - Génération automatique matricule (EL202400123)
  - Upload photo élève
  - Dossier scolaire complet (onglets : Info, Finances, Notes, Documents)
  - Recherche et filtres avancés

- **Module Financier**
  - Configuration types de frais (scolarité, cantine, etc.)
  - Génération automatique frais à l'inscription
  - Enregistrement paiement avec ventilation multi-enfants
  - Bulletin financier par élève
  - Dashboard financier (KPIs, Top impayés)
  - Génération reçus PDF

- **Génération de Documents**
  - Carte d'élève (photo + QR code)
  - Reçu de paiement
  - Bulletin financier
  - Certificat de scolarité

- **Module Académique (Basique)**
  - Saisie notes par matière et période
  - Calcul automatique moyennes
  - Bulletin de notes PDF

- **Dashboard & Reporting**
  - KPIs temps réel
  - Graphiques financiers
  - Export Excel

- **Infrastructure**
  - Architecture Clean Architecture + CQRS
  - Entity Framework Core 8
  - SQL Server Express
  - Tests unitaires (70% coverage)
  - CI/CD GitHub Actions

#### 🔧 Technique
- .NET 8.0
- WPF (Windows Presentation Foundation)
- Entity Framework Core 8.0
- MediatR (CQRS)
- FluentValidation
- BCrypt.Net
- QuestPDF (génération PDF)
- QRCoder (QR codes)
- ClosedXML (export Excel)

---

## [0.5.0] - 2025-01-15

### 🧪 Version Beta

#### ✨ Ajouté
- Beta testing avec 3 établissements
- Module financier complet
- Génération documents PDF

#### 🐛 Corrigé
- Correction calcul ventilation paiements
- Fix génération matricule en doublon
- Amélioration performance recherche élèves

#### ⚡ Amélioré
- Interface utilisateur plus intuitive
- Validation formulaires temps réel
- Messages d'erreur plus explicites

---

## [0.3.0] - 2025-01-08

### 🏗️ Version Alpha

#### ✨ Ajouté
- Gestion familles et élèves
- Authentification basique
- Base de données SQL Server

#### 🔧 Technique
- Setup CI/CD
- Tests unitaires initiaux
- Documentation technique

---

## [0.1.0] - 2025-01-01

### 🚀 Première version (POC)

#### ✨ Ajouté
- Proof of Concept
- Architecture de base
- Login simple
- CRUD Familles minimal

---

## Types de changements

- `✨ Ajouté` : Nouvelles fonctionnalités
- `🔧 Modifié` : Changements dans les fonctionnalités existantes
- `⚠️ Déprécié` : Fonctionnalités bientôt supprimées
- `❌ Supprimé` : Fonctionnalités supprimées
- `🐛 Corrigé` : Corrections de bugs
- `🔒 Sécurité` : Correctifs de sécurité
- `⚡ Amélioré` : Améliorations de performance

---

[Unreleased]: https://github.com/Heinkek-99/GMS-School-Management/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/Heinkek-99/GMS-School-Management/releases/tag/v1.0.0
[0.5.0]: https://github.com/Heinkek-99/GMS-School-Management/releases/tag/v0.5.0
[0.3.0]: https://github.com/Heinkek-99/GMS-School-Management/releases/tag/v0.3.0
[0.1.0]: https://github.com/Heinkek-99/GMS-School-Management/releases/tag/v0.1.0