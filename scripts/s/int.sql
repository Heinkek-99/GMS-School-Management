-- ==========================================
-- Script d'initialisation GMS Database
-- Compatible avec la migration InitialCreate
-- ==========================================

USE GMSDatabase;
GO

-- Vérifier si les tables existent
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Utilisateurs')
BEGIN
    PRINT 'Les tables n''existent pas encore. Exécutez d''abord les migrations EF Core.';
    RETURN;
END
GO

-- ==========================================
-- SEED DATA: Utilisateurs par défaut
-- ==========================================

PRINT '🌱 Insertion des utilisateurs par défaut...';

-- Admin
IF NOT EXISTS (SELECT 1 FROM Utilisateurs WHERE Username = 'admin')
BEGIN
    INSERT INTO Utilisateurs (
        Id, Nom, Prenom, Email, Username, PasswordHash, Role, 
        EstActif, CreatedAt, CreatedBy, IsDeleted
    )
    VALUES (
        NEWID(),
        'ADMIN',
        'Super',
        'admin@gms.com',
        'admin',
        '$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewY5L.zO8UDWDlAa', -- Admin@123
        'Admin',
        1,
        GETUTCDATE(),
        'System',
        0
    );
    PRINT '✅ Utilisateur Admin créé';
END
ELSE
    PRINT '⚠️  Utilisateur Admin existe déjà';

-- Directeur
IF NOT EXISTS (SELECT 1 FROM Utilisateurs WHERE Username = 'directeur')
BEGIN
    INSERT INTO Utilisateurs (
        Id, Nom, Prenom, Email, Username, PasswordHash, Role, 
        EstActif, CreatedAt, CreatedBy, IsDeleted
    )
    VALUES (
        NEWID(),
        'KOFFI',
        'Marie',
        'directeur@gms.com',
        'directeur',
        '$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewY5L.zO8UDWDlAa', -- Direct@123
        'Directeur',
        1,
        GETUTCDATE(),
        'System',
        0
    );
    PRINT '✅ Utilisateur Directeur créé';
END

-- Secrétaire
IF NOT EXISTS (SELECT 1 FROM Utilisateurs WHERE Username = 'secretaire')
BEGIN
    INSERT INTO Utilisateurs (
        Id, Nom, Prenom, Email, Username, PasswordHash, Role, 
        EstActif, CreatedAt, CreatedBy, IsDeleted
    )
    VALUES (
        NEWID(),
        'TRAORE',
        'Fatou',
        'secretaire@gms.com',
        'secretaire',
        '$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewY5L.zO8UDWDlAa', -- Secret@123
        'Secretaire',
        1,
        GETUTCDATE(),
        'System',
        0
    );
    PRINT '✅ Utilisateur Secrétaire créé';
END

-- Comptable
IF NOT EXISTS (SELECT 1 FROM Utilisateurs WHERE Username = 'comptable')
BEGIN
    INSERT INTO Utilisateurs (
        Id, Nom, Prenom, Email, Username, PasswordHash, Role, 
        EstActif, CreatedAt, CreatedBy, IsDeleted
    )
    VALUES (
        NEWID(),
        'OUATTARA',
        'Ibrahim',
        'comptable@gms.com',
        'comptable',
        '$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewY5L.zO8UDWDlAa', -- Compta@123
        'Comptable',
        1,
        GETUTCDATE(),
        'System',
        0
    );
    PRINT '✅ Utilisateur Comptable créé';
END

GO

-- ==========================================
-- SEED DATA: Année Scolaire
-- ==========================================

PRINT '🌱 Insertion année scolaire...';

DECLARE @AnneeScolaireId UNIQUEIDENTIFIER;

IF NOT EXISTS (SELECT 1 FROM AnneesScolaires WHERE Libelle = '2024-2025')
BEGIN
    SET @AnneeScolaireId = NEWID();
    
    INSERT INTO AnneesScolaires (
        Id, Libelle, DateDebut, DateFin, EstActive, 
        CreatedAt, CreatedBy, IsDeleted
    )
    VALUES (
        @AnneeScolaireId,
        '2024-2025',
        '2024-09-01',
        '2025-06-30',
        1,
        GETUTCDATE(),
        'System',
        0
    );
    
    -- Périodes (Trimestres)
    INSERT INTO Periodes (
        Id, Libelle, Numero, DateDebut, DateFin, AnneeScolaireId, 
        CreatedAt, CreatedBy, IsDeleted
    )
    VALUES 
    (NEWID(), 'Trimestre 1', 1, '2024-09-01', '2024-12-15', @AnneeScolaireId, GETUTCDATE(), 'System', 0),
    (NEWID(), 'Trimestre 2', 2, '2025-01-05', '2025-03-31', @AnneeScolaireId, GETUTCDATE(), 'System', 0),
    (NEWID(), 'Trimestre 3', 3, '2025-04-01', '2025-06-30', @AnneeScolaireId, GETUTCDATE(), 'System', 0);
    
    PRINT '✅ Année scolaire 2024-2025 créée avec 3 trimestres';
END
ELSE
    PRINT '⚠️  Année scolaire 2024-2025 existe déjà';

GO

-- ==========================================
-- SEED DATA: Classes
-- ==========================================

PRINT '🌱 Insertion des classes...';

IF NOT EXISTS (SELECT 1 FROM Classes WHERE Nom = 'CP')
BEGIN
    INSERT INTO Classes (
        Id, Nom, Niveau, Ordre, EffectifMax, 
        CreatedAt, CreatedBy, IsDeleted
    )
    VALUES 
    -- Primaire
    (NEWID(), 'CP', 'Primaire', 1, 30, GETUTCDATE(), 'System', 0),
    (NEWID(), 'CE1', 'Primaire', 2, 30, GETUTCDATE(), 'System', 0),
    (NEWID(), 'CE2', 'Primaire', 3, 30, GETUTCDATE(), 'System', 0),
    (NEWID(), 'CM1', 'Primaire', 4, 35, GETUTCDATE(), 'System', 0),
    (NEWID(), 'CM2', 'Primaire', 5, 35, GETUTCDATE(), 'System', 0),
    
    -- Collège
    (NEWID(), '6ème', 'Collège', 6, 40, GETUTCDATE(), 'System', 0),
    (NEWID(), '5ème', 'Collège', 7, 40, GETUTCDATE(), 'System', 0),
    (NEWID(), '4ème', 'Collège', 8, 40, GETUTCDATE(), 'System', 0),
    (NEWID(), '3ème', 'Collège', 9, 40, GETUTCDATE(), 'System', 0),
    
    -- Lycée
    (NEWID(), '2nde', 'Lycée', 10, 45, GETUTCDATE(), 'System', 0),
    (NEWID(), '1ère', 'Lycée', 11, 45, GETUTCDATE(), 'System', 0),
    (NEWID(), 'Terminale', 'Lycée', 12, 45, GETUTCDATE(), 'System', 0);
    
    PRINT '✅ 12 classes créées (CP à Terminale)';
END
ELSE
    PRINT '⚠️  Classes déjà existantes';

GO

-- ==========================================
-- SEED DATA: Matières
-- ==========================================

PRINT '🌱 Insertion des matières...';

IF NOT EXISTS (SELECT 1 FROM Matieres WHERE Code = 'FR')
BEGIN
    INSERT INTO Matieres (
        Id, Code, Libelle, Coefficient, Couleur, 
        CreatedAt, CreatedBy, IsDeleted
    )
    VALUES 
    (NEWID(), 'FR', 'Français', 4, '#3B82F6', GETUTCDATE(), 'System', 0),
    (NEWID(), 'MATH', 'Mathématiques', 4, '#10B981', GETUTCDATE(), 'System', 0),
    (NEWID(), 'ANG', 'Anglais', 3, '#F59E0B', GETUTCDATE(), 'System', 0),
    (NEWID(), 'SVT', 'Sciences de la Vie et de la Terre', 3, '#8B5CF6', GETUTCDATE(), 'System', 0),
    (NEWID(), 'PC', 'Physique-Chimie', 3, '#EF4444', GETUTCDATE(), 'System', 0),
    (NEWID(), 'HG', 'Histoire-Géographie', 3, '#EC4899', GETUTCDATE(), 'System', 0),
    (NEWID(), 'EPS', 'Éducation Physique et Sportive', 2, '#06B6D4', GETUTCDATE(), 'System', 0),
    (NEWID(), 'INFO', 'Informatique', 2, '#6366F1', GETUTCDATE(), 'System', 0);
    
    PRINT '✅ 8 matières créées';
END
ELSE
    PRINT '⚠️  Matières déjà existantes';

GO

-- ==========================================
-- SEED DATA: Types de Frais
-- ==========================================

PRINT '🌱 Insertion des types de frais...';

IF NOT EXISTS (SELECT 1 FROM TypesFrais WHERE Code = 'INSC')
BEGIN
    INSERT INTO TypesFrais (
        Id, Code, Libelle, Description, EstRecurrent, Frequence, 
        MontantParDefaut, CreatedAt, CreatedBy, IsDeleted
    )
    VALUES 
    (NEWID(), 'INSC', 'Frais d''inscription', 'Frais d''inscription annuelle', 0, 'Annuel', 50000, GETUTCDATE(), 'System', 0),
    (NEWID(), 'SCOL', 'Scolarité', 'Frais de scolarité mensuelle', 1, 'Mensuel', 75000, GETUTCDATE(), 'System', 0),
    (NEWID(), 'CANT', 'Cantine', 'Frais de cantine mensuelle', 1, 'Mensuel', 25000, GETUTCDATE(), 'System', 0),
    (NEWID(), 'UNIF', 'Uniforme', 'Uniforme scolaire', 0, 'Annuel', 35000, GETUTCDATE(), 'System', 0),
    (NEWID(), 'MANU', 'Manuels scolaires', 'Fournitures et manuels', 0, 'Annuel', 40000, GETUTCDATE(), 'System', 0),
    (NEWID(), 'TRAN', 'Transport', 'Frais de transport scolaire', 1, 'Mensuel', 30000, GETUTCDATE(), 'System', 0);
    
    PRINT '✅ 6 types de frais créés';
END
ELSE
    PRINT '⚠️  Types de frais déjà existants';

GO

-- ==========================================
-- FIN DU SCRIPT
-- ==========================================

PRINT '';
PRINT '================================================';
PRINT '✅ Initialisation de la base GMSDatabase terminée!';
PRINT '================================================';
PRINT '';
PRINT '📊 Données créées:';
PRINT '   - 4 utilisateurs par défaut';
PRINT '   - 1 année scolaire (2024-2025)';
PRINT '   - 3 périodes (trimestres)';
PRINT '   - 12 classes (CP à Terminale)';
PRINT '   - 8 matières';
PRINT '   - 6 types de frais';
PRINT '';
PRINT '🔐 Identifiants de connexion:';
PRINT '   admin      / Admin@123';
PRINT '   directeur  / Direct@123';
PRINT '   secretaire / Secret@123';
PRINT '   comptable  / Compta@123';
PRINT '';
PRINT '⚠️  IMPORTANT: Tous les mots de passe sont identiques pour le test!';
PRINT '    Hash BCrypt utilisé: Admin@123, Direct@123, etc.';
PRINT '================================================';

GO