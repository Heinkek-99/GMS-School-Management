-- ==========================================
-- Script d'initialisation GMS Database
-- ==========================================

USE GmsDb;
GO

-- Vérifier si les données existent déjà
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Utilisateurs')
BEGIN
    PRINT 'Les tables n''existent pas encore. Les migrations EF Core les créeront.'
    RETURN;
END
GO

-- ==========================================
-- SEED DATA: Utilisateurs par défaut
-- ==========================================

PRINT '🌱 Insertion des utilisateurs par défaut...';

-- Vérifier si les utilisateurs existent déjà
IF NOT EXISTS (SELECT 1 FROM Utilisateurs WHERE Username = 'admin')
BEGIN
    INSERT INTO Utilisateurs (Id, Nom, Prenom, Email, Username, PasswordHash, Role, EstActif, CreatedAt, IsDeleted)
    VALUES 
    (
        NEWID(),
        'ADMIN',
        'System',
        'admin@gms.local',
        'admin',
        '$2a$11$YourBCryptHashHere',  -- À remplacer par le vrai hash
        'Admin',
        1,
        GETDATE(),
        0
    );
    PRINT '✅ Utilisateur Admin créé';
END
ELSE
BEGIN
    PRINT '⚠️  Utilisateur Admin existe déjà';
END

IF NOT EXISTS (SELECT 1 FROM Utilisateurs WHERE Username = 'directeur')
BEGIN
    INSERT INTO Utilisateurs (Id, Nom, Prenom, Email, Username, PasswordHash, Role, EstActif, CreatedAt, IsDeleted)
    VALUES 
    (
        NEWID(),
        'KOFFI',
        'Jean',
        'directeur@gms.local',
        'directeur',
        '$2a$11$YourBCryptHashHere',
        'Directeur',
        1,
        GETDATE(),
        0
    );
    PRINT '✅ Utilisateur Directeur créé';
END

IF NOT EXISTS (SELECT 1 FROM Utilisateurs WHERE Username = 'secretaire')
BEGIN
    INSERT INTO Utilisateurs (Id, Nom, Prenom, Email, Username, PasswordHash, Role, EstActif, CreatedAt, IsDeleted)
    VALUES 
    (
        NEWID(),
        'TRAORE',
        'Marie',
        'secretaire@gms.local',
        'secretaire',
        '$2a$11$YourBCryptHashHere',
        'Secretaire',
        1,
        GETDATE(),
        0
    );
    PRINT '✅ Utilisateur Secrétaire créé';
END

IF NOT EXISTS (SELECT 1 FROM Utilisateurs WHERE Username = 'comptable')
BEGIN
    INSERT INTO Utilisateurs (Id, Nom, Prenom, Email, Username, PasswordHash, Role, EstActif, CreatedAt, IsDeleted)
    VALUES 
    (
        NEWID(),
        'DIALLO',
        'Amadou',
        'comptable@gms.local',
        'comptable',
        '$2a$11$YourBCryptHashHere',
        'Comptable',
        1,
        GETDATE(),
        0
    );
    PRINT '✅ Utilisateur Comptable créé';
END

GO

-- ==========================================
-- SEED DATA: Année Scolaire
-- ==========================================

PRINT '🌱 Insertion année scolaire...';

IF NOT EXISTS (SELECT 1 FROM AnneesScolaires WHERE Libelle = '2024-2025')
BEGIN
    DECLARE @AnneeScolaireId UNIQUEIDENTIFIER = NEWID();
    
    INSERT INTO AnneesScolaires (Id, Libelle, DateDebut, DateFin, EstActive, CreatedAt, IsDeleted)
    VALUES 
    (
        @AnneeScolaireId,
        '2024-2025',
        '2024-09-01',
        '2025-06-30',
        1,
        GETDATE(),
        0
    );
    
    -- Périodes (Trimestres)
    INSERT INTO Periodes (Id, Libelle, Numero, DateDebut, DateFin, AnneeScolaireId, CreatedAt, IsDeleted)
    VALUES 
    (NEWID(), 'Trimestre 1', 1, '2024-09-01', '2024-12-15', @AnneeScolaireId, GETDATE(), 0),
    (NEWID(), 'Trimestre 2', 2, '2025-01-05', '2025-03-31', @AnneeScolaireId, GETDATE(), 0),
    (NEWID(), 'Trimestre 3', 3, '2025-04-01', '2025-06-30', @AnneeScolaireId, GETDATE(), 0);
    
    PRINT '✅ Année scolaire 2024-2025 créée avec 3 trimestres';
END
ELSE
BEGIN
    PRINT '⚠️  Année scolaire 2024-2025 existe déjà';
END

GO

-- ==========================================
-- SEED DATA: Classes
-- ==========================================

PRINT '🌱 Insertion des classes...';

IF NOT EXISTS (SELECT 1 FROM Classes WHERE Nom = 'CP')
BEGIN
    INSERT INTO Classes (Id, Nom, Niveau, Ordre, EffectifMax, CreatedAt, IsDeleted)
    VALUES 
    -- Primaire
    (NEWID(), 'CP', 'Primaire', 1, 40, GETDATE(), 0),
    (NEWID(), 'CE1', 'Primaire', 2, 40, GETDATE(), 0),
    (NEWID(), 'CE2', 'Primaire', 3, 40, GETDATE(), 0),
    (NEWID(), 'CM1', 'Primaire', 4, 40, GETDATE(), 0),
    (NEWID(), 'CM2', 'Primaire', 5, 40, GETDATE(), 0),
    
    -- Collège
    (NEWID(), '6ème', 'Collège', 6, 45, GETDATE(), 0),
    (NEWID(), '5ème', 'Collège', 7, 45, GETDATE(), 0),
    (NEWID(), '4ème', 'Collège', 8, 45, GETDATE(), 0),
    (NEWID(), '3ème', 'Collège', 9, 45, GETDATE(), 0),
    
    -- Lycée
    (NEWID(), '2nde', 'Lycée', 10, 50, GETDATE(), 0),
    (NEWID(), '1ère', 'Lycée', 11, 50, GETDATE(), 0),
    (NEWID(), 'Terminale', 'Lycée', 12, 50, GETDATE(), 0);
    
    PRINT '✅ 12 classes créées (CP à Terminale)';
END
ELSE
BEGIN
    PRINT '⚠️  Classes déjà existantes';
END

GO

-- ==========================================
-- SEED DATA: Matières
-- ==========================================

PRINT '🌱 Insertion des matières...';

IF NOT EXISTS (SELECT 1 FROM Matieres WHERE Code = 'FR')
BEGIN
    INSERT INTO Matieres (Id, Code, Libelle, Coefficient, Couleur, CreatedAt, IsDeleted)
    VALUES 
    (NEWID(), 'FR', 'Français', 3, '#3B82F6', GETDATE(), 0),
    (NEWID(), 'MATH', 'Mathématiques', 3, '#10B981', GETDATE(), 0),
    (NEWID(), 'ANG', 'Anglais', 2, '#F59E0B', GETDATE(), 0),
    (NEWID(), 'SVT', 'Sciences de la Vie et de la Terre', 2, '#8B5CF6', GETDATE(), 0),
    (NEWID(), 'PC', 'Physique-Chimie', 2, '#EF4444', GETDATE(), 0),
    (NEWID(), 'HG', 'Histoire-Géographie', 2, '#EC4899', GETDATE(), 0),
    (NEWID(), 'EPS', 'Éducation Physique et Sportive', 1, '#06B6D4', GETDATE(), 0),
    (NEWID(), 'ART', 'Arts Plastiques', 1, '#F97316', GETDATE(), 0),
    (NEWID(), 'INFO', 'Informatique', 1, '#6366F1', GETDATE(), 0),
    (NEWID(), 'PHILO', 'Philosophie', 3, '#84CC16', GETDATE(), 0);
    
    PRINT '✅ 10 matières créées';
END
ELSE
BEGIN
    PRINT '⚠️  Matières déjà existantes';
END

GO

-- ==========================================
-- SEED DATA: Types de Frais
-- ==========================================

PRINT '🌱 Insertion des types de frais...';

IF NOT EXISTS (SELECT 1 FROM TypesFrais WHERE Code = 'INSC')
BEGIN
    INSERT INTO TypesFrais (Id, Code, Libelle, Description, EstRecurrent, Frequence, CreatedAt, IsDeleted)
    VALUES 
    (NEWID(), 'INSC', 'Frais d''inscription', 'Frais d''inscription annuelle', 0, 'Annuel', GETDATE(), 0),
    (NEWID(), 'SCOL', 'Scolarité', 'Frais de scolarité annuelle', 1, 'Annuel', GETDATE(), 0),
    (NEWID(), 'CANT', 'Cantine', 'Frais de cantine', 1, 'Mensuel', GETDATE(), 0),
    (NEWID(), 'TRANS', 'Transport', 'Frais de transport scolaire', 1, 'Mensuel', GETDATE(), 0),
    (NEWID(), 'FOUR', 'Fournitures', 'Fournitures scolaires', 0, 'Annuel', GETDATE(), 0),
    (NEWID(), 'UNIF', 'Uniforme', 'Uniforme scolaire', 0, 'Annuel', GETDATE(), 0),
    (NEWID(), 'EXAM', 'Examens', 'Frais d''examens officiels', 0, 'Ponctuel', GETDATE(), 0),
    (NEWID(), 'ACT', 'Activités parascolaires', 'Clubs, sorties, activités extra-scolaires', 0, 'Ponctuel', GETDATE(), 0);
    
    PRINT '✅ 8 types de frais créés';
END
ELSE
BEGIN
    PRINT '⚠️  Types de frais déjà existants';
END

GO

-- ==========================================
-- FIN DU SCRIPT
-- ==========================================

PRINT '';
PRINT '================================================';
PRINT '✅ Initialisation de la base GmsDb terminée!';
PRINT '================================================';
PRINT '';
PRINT '📊 Données créées:';
PRINT '   - 4 utilisateurs par défaut';
PRINT '   - 1 année scolaire (2024-2025)';
PRINT '   - 3 périodes (trimestres)';
PRINT '   - 12 classes (CP à Terminale)';
PRINT '   - 10 matières';
PRINT '   - 8 types de frais';
PRINT '';
PRINT '🔐 Identifiants:';
PRINT '   admin / Admin@123';
PRINT '   directeur / Dir@123';
PRINT '   secretaire / Sec@123';
PRINT '   comptable / Compta@123';
PRINT '';
PRINT '⚠️  IMPORTANT: Changez ces mots de passe en production!';
PRINT '================================================';

GO