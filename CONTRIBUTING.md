# 🤝 Guide de Contribution - GMS School Management

Merci de votre intérêt pour contribuer à GMS ! 🎉

## 📋 Table des matières

- [Code de Conduite](#code-de-conduite)
- [Comment puis-je contribuer ?](#comment-puis-je-contribuer)
- [Processus de développement](#processus-de-développement)
- [Standards de code](#standards-de-code)
- [Processus de Pull Request](#processus-de-pull-request)
- [Reporting de bugs](#reporting-de-bugs)
- [Suggestions de fonctionnalités](#suggestions-de-fonctionnalités)

---

## 📜 Code de Conduite

Ce projet adhère au [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). En participant, vous êtes censé respecter ce code.

---

## 🎯 Comment puis-je contribuer ?

### Types de contributions acceptées

- 🐛 **Corrections de bugs**
- ✨ **Nouvelles fonctionnalités**
- 📝 **Amélioration de la documentation**
- 🎨 **Améliorations UI/UX**
- ⚡ **Optimisations de performance**
- 🧪 **Ajout de tests**
- 🌍 **Traductions** (prévu pour V2)

---

## 🔄 Processus de développement

### 1. Fork & Clone

```bash
# Fork le repository sur GitHub (bouton Fork)

# Cloner votre fork
git clone https://github.com/VOTRE-USERNAME/GMS-School-Management.git
cd GMS-School-Management

# Ajouter le repository original comme remote
git remote add upstream https://github.com/Heinkek-99/GMS-School-Management.git
```

### 2. Créer une branche

```bash
# Se synchroniser avec upstream
git checkout develop
git pull upstream develop

# Créer une branche feature
git checkout -b feature/nom-de-votre-feature

# OU pour un bugfix
git checkout -b bugfix/nom-du-bug
```

### 3. Développer

```bash
# Faire vos modifications

# Exécuter les tests
dotnet test

# Vérifier le formatage
dotnet format

# Commit avec message conventionnel
git commit -m "feat: Description de votre feature"
```

### 4. Push et Pull Request

```bash
# Pusher vers votre fork
git push origin feature/nom-de-votre-feature

# Créer une Pull Request sur GitHub
# Base: Heinkek-99/develop ← Head: VOTRE-USERNAME/feature/nom-de-votre-feature
```

---

## 📏 Standards de code

### Conventions de nommage

**C# / .NET**
```csharp
// Classes : PascalCase
public class FamilleService { }

// Méthodes publiques : PascalCase
public void CreateFamille() { }

// Méthodes privées : PascalCase
private void ValidateInput() { }

// Variables : camelCase
private string familyName;

// Constantes : UPPERCASE
private const string DEFAULT_CURRENCY = "FCFA";

// Interfaces : I prefix
public interface IFamilleRepository { }
```

**XAML**
```xml
<!-- Fichiers : PascalCase -->
<!-- LoginView.xaml -->

<!-- x:Name : camelCase -->
<TextBox x:Name="usernameTextBox" />

<!-- Resources : PascalCase -->
<Style x:Key="PrimaryButton" />
```

### Règles de formatage

- **Indentation** : 4 espaces (pas de tabs)
- **Ligne max** : 120 caractères
- **Accolades** : Nouvelle ligne (style Allman)
- **Using** : Ordonnés alphabétiquement

```csharp
// ✅ BON
public class Example
{
    public void Method()
    {
        if (condition)
        {
            DoSomething();
        }
    }
}

// ❌ MAUVAIS
public class Example {
    public void Method() {
        if (condition) {
            DoSomething();
        }
    }
}
```

### Commentaires

```csharp
// ✅ XML Comments pour méthodes publiques
/// <summary>
/// Crée une nouvelle famille dans le système.
/// </summary>
/// <param name="command">Données de la famille à créer</param>
/// <returns>ID de la famille créée</returns>
public async Task<Guid> CreateFamille(CreateFamilleCommand command)
{
    // Commentaires inline pour logique complexe uniquement
    // ...
}
```

---

## 🔀 Processus de Pull Request

### Checklist avant de soumettre

- [ ] Le code build sans erreur (`dotnet build`)
- [ ] Tous les tests passent (`dotnet test`)
- [ ] Code formaté (`dotnet format`)
- [ ] Pas de warning du compilateur
- [ ] Documentation mise à jour si nécessaire
- [ ] CHANGELOG.md mis à jour
- [ ] Commits respectent Conventional Commits

### Template de Pull Request

```markdown
## Description
[Description claire de ce que fait la PR]

## Type de changement
- [ ] 🐛 Bug fix (changement qui corrige un problème)
- [ ] ✨ Nouvelle fonctionnalité (changement qui ajoute une fonctionnalité)
- [ ] 💥 Breaking change (correction ou fonctionnalité qui change le comportement existant)
- [ ] 📝 Documentation (changement dans la documentation uniquement)

## Comment a été testé ?
[Décrivez les tests effectués]

## Screenshots (si applicable)
[Ajoutez des captures d'écran]

## Checklist
- [ ] Mon code suit les standards du projet
- [ ] J'ai effectué une auto-revue de mon code
- [ ] J'ai commenté les parties complexes
- [ ] J'ai mis à jour la documentation
- [ ] Mes changements ne génèrent pas de nouveaux warnings
- [ ] J'ai ajouté des tests qui prouvent que ma correction fonctionne
- [ ] Les tests unitaires existants passent
```

### Processus de review

1. **Soumission** : Vous créez la PR
2. **CI/CD** : Les tests automatiques s'exécutent
3. **Review** : Un mainteneur revoit votre code
4. **Feedback** : Vous apportez les corrections demandées
5. **Approbation** : La PR est approuvée
6. **Merge** : Un mainteneur merge dans `develop`

**Délai de review :** Maximum 3 jours ouvrés

---

## 🐛 Reporting de bugs

### Avant de reporter

1. **Vérifiez** que le bug n'a pas déjà été reporté dans [Issues](https://github.com/Heinkek-99/GMS-School-Management/issues)
2. **Vérifiez** que vous utilisez la dernière version
3. **Essayez** de reproduire le bug

### Template de bug report

**Titre :** [Bug] Description courte

**Description :**
Description détaillée du problème

**Étapes pour reproduire :**
1. Aller sur '...'
2. Cliquer sur '...'
3. Voir l'erreur

**Comportement attendu :**
Ce qui devrait se passer

**Comportement actuel :**
Ce qui se passe réellement

**Screenshots :**
Si applicable

**Environnement :**
- OS : Windows 10 Pro 22H2
- Version GMS : 1.0.0
- .NET Version : 8.0.1

**Logs :**
```
Copier les logs pertinents ici
```

---

## 💡 Suggestions de fonctionnalités

### Template de feature request

**Titre :** [Feature] Description courte

**Problème à résoudre :**
Décrivez le problème que cette fonctionnalité résoudrait

**Solution proposée :**
Décrivez comment vous voyez cette fonctionnalité

**Alternatives considérées :**
Autres solutions envisagées

**Contexte additionnel :**
Ajoutez tout autre contexte ou screenshots

---

## 🏷️ Labels des Issues

| Label | Description |
|-------|-------------|
| `bug` | Quelque chose ne fonctionne pas |
| `enhancement` | Nouvelle fonctionnalité ou demande |
| `documentation` | Amélioration de la documentation |
| `good first issue` | Bon pour les nouveaux contributeurs |
| `help wanted` | Aide externe demandée |
| `question` | Question sur le projet |
| `wontfix` | Ne sera pas corrigé |
| `duplicate` | Problème déjà reporté |
| `P0` | Priorité critique |
| `P1` | Priorité haute |
| `P2` | Priorité moyenne |

---

## 🧪 Tests

### Exécuter les tests localement

```bash
# Tous les tests
dotnet test

# Tests d'un projet spécifique
dotnet test src/GMS.Tests/GMS.Tests.csproj

# Avec couverture
dotnet test --collect:"XPlat Code Coverage"

# Générer rapport HTML
dotnet tool install --global dotnet-reportgenerator-globaltool
reportgenerator -reports:./TestResults/*/coverage.cobertura.xml -targetdir:./coverage-report
```

### Écrire des tests

```csharp
// Convention de nommage des tests
// MethodName_Scenario_ExpectedBehavior

[Fact]
public void CreateFamille_WithValidData_ShouldReturnFamilleId()
{
    // Arrange
    var command = new CreateFamilleCommand { /* ... */ };
    
    // Act
    var result = await _handler.Handle(command);
    
    // Assert
    result.IsSuccess.Should().BeTrue();
    result.Data.Should().NotBeEmpty();
}
```

**Objectif de couverture :** 70% minimum

---

## 📦 Conventions de commit

Nous suivons [Conventional Commits](https://www.conventionalcommits.org/) :

### Format

```
<type>(<scope>): <description>

[optional body]

[optional footer]
```

### Types

- `feat`: Nouvelle fonctionnalité
- `fix`: Correction de bug
- `docs`: Documentation uniquement
- `style`: Changements de formatage (espaces, virgules, etc.)
- `refactor`: Refactorisation sans changement fonctionnel
- `perf`: Amélioration de performance
- `test`: Ajout ou modification de tests
- `chore`: Maintenance (build, config, etc.)
- `ci`: Changements CI/CD

### Scopes (optionnels)

- `eleve`: Module élèves
- `famille`: Module familles
- `finance`: Module financier
- `auth`: Authentification
- `ui`: Interface utilisateur
- `db`: Base de données

### Exemples

```bash
feat(eleve): add photo upload functionality
fix(finance): correct payment ventilation calculation
docs: update installation guide
refactor(auth): simplify login logic
test(famille): add unit tests for famille service
chore: update dependencies
```

---

## 🔒 Sécurité

Si vous découvrez une **faille de sécurité**, **NE PAS** créer une issue publique.

**Envoyez un email à :** security@gms-school.com

Nous vous répondrons dans les 48 heures.

---

## 📞 Questions ?

- 💬 [Discord](https://discord.gg/gms)
- 📧 contribute@gms-school.com
- 📖 [Wiki](https://github.com/Heinkek-99/GMS-School-Management/wiki)

---

## 🎉 Remerciements

Merci à tous nos contributeurs qui rendent ce projet possible ! ❤️

<a href="https://github.com/Heinkek-99/GMS-School-Management/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=Heinkek-99/GMS-School-Management" />
</a>

---

**Bonne contribution ! 🚀**