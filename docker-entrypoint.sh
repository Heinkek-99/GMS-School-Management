#!/bin/bash
set -e

echo "================================================"
echo "  GMS - Gestion Management School"
echo "  Docker Entrypoint"
echo "================================================"

# Fonction pour attendre SQL Server
wait_for_sqlserver() {
    echo "⏳ Attente de SQL Server..."
    local max_attempts=30
    local attempt=1
    
    while [ $attempt -le $max_attempts ]; do
        echo "   Tentative $attempt/$max_attempts..."
        
        if /opt/mssql-tools/bin/sqlcmd \
            -S sqlserver,1433 \
            -U sa \
            -P "Azerty12" \
            -Q "SELECT 1" > /dev/null 2>&1; then
            echo "✅ SQL Server est prêt!"
            return 0
        fi
        
        attempt=$((attempt + 1))
        sleep 2
    done
    
    echo "❌ Impossible de se connecter à SQL Server après ${max_attempts} tentatives"
    exit 1
}

# Fonction pour créer la base de données
create_database() {
    echo "📊 Création de la base de données GmsDb..."
    
    /opt/mssql-tools/bin/sqlcmd \
        -S sqlserver,1433 \
        -U sa \
        -P "Azerty12" \
        -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'GmsDb') CREATE DATABASE GmsDb"
    
    if [ $? -eq 0 ]; then
        echo "✅ Base de données GmsDb créée/vérifiée"
    else
        echo "❌ Erreur lors de la création de la base de données"
        exit 1
    fi
}

# Fonction pour appliquer les migrations EF Core
apply_migrations() {
    echo "🔄 Application des migrations EF Core..."
    
    cd /app
    
    # Vérifier si dotnet-ef est disponible
    if ! command -v dotnet-ef &> /dev/null; then
        echo "⚠️  dotnet-ef non trouvé, installation..."
        dotnet tool install --global dotnet-ef
        export PATH="$PATH:/root/.dotnet/tools"
    fi
    
    # Attendre un peu pour être sûr que la base est prête
    sleep 5
    
    # Appliquer les migrations
    echo "   Exécution: dotnet ef database update..."
    
    # Note: Adapter le chemin selon votre structure
    # Si vous avez un projet de migration dédié
    if [ -f "GMS.Infrastructure.dll" ]; then
        dotnet ef database update \
            --assembly GMS.Infrastructure.dll \
            --startup-assembly GMS.Desktop.dll \
            --verbose || {
            echo "⚠️  Migrations non appliquées (peut-être déjà à jour)"
        }
    else
        echo "⚠️  Impossible de trouver les assemblies pour les migrations"
        echo "   Les migrations devront être appliquées manuellement"
    fi
}

# Fonction pour insérer les données de seed
seed_data() {
    echo "🌱 Insertion des données initiales (seed)..."
    
    /opt/mssql-tools/bin/sqlcmd \
        -S sqlserver,1433 \
        -U sa \
        -P "Azerty12" \
        -d GmsDb \
        -i /docker-entrypoint-initdb.d/init-db.sql || {
        echo "⚠️  Seed data non inséré (peut-être déjà présent)"
    }
}

# Fonction principale
main() {
    echo ""
    echo "🚀 Démarrage de GMS..."
    echo ""
    
    # Étape 1: Attendre SQL Server
    wait_for_sqlserver
    
    # Étape 2: Créer la base de données
    create_database
    
    # Étape 3: Appliquer les migrations
    apply_migrations
    
    # Étape 4: Seed data
    seed_data
    
    echo ""
    echo "================================================"
    echo "✅ GMS est prêt!"
    echo "================================================"
    echo ""
    echo "📍 Connexion SQL Server:"
    echo "   Host: localhost"
    echo "   Port: 1433"
    echo "   User: sa"
    echo "   Pass: Azerty12!"
    echo "   DB:   GmsDb"
    echo ""
    echo "👤 Identifiants par défaut:"
    echo "   Admin      : admin / Admin@123"
    echo "   Directeur  : directeur / Dir@123"
    echo "   Secrétaire : secretaire / Sec@123"
    echo "   Comptable  : comptable / Compta@123"
    echo ""
    echo "================================================"
    
    # Étape 5: Lancer l'application
    echo "🎯 Démarrage de l'application GMS.Desktop..."
    echo ""
    
    # Pour une app WPF, on ne peut pas vraiment la lancer dans Docker
    # Cette ligne est symbolique - en production, vous utiliserez plutôt une API
    # Pour le moment, on garde le conteneur vivant
    
    # Si c'est une API Web:
    # exec dotnet GMS.Desktop.dll
    
    # Pour WPF (mode développement Docker):
    echo "⚠️  Note: GMS.Desktop est une application WPF (interface graphique)"
    echo "   Elle ne peut pas s'exécuter directement dans Docker."
    echo "   Utilisez ce conteneur pour:"
    echo "   - Base de données SQL Server (accessible via localhost:1433)"
    echo "   - Tests d'intégration"
    echo ""
    echo "   Pour l'application desktop, lancez depuis votre machine:"
    echo "   dotnet run --project src/GMS.Desktop/GMS.Desktop.csproj"
    echo ""
    
    # Garder le conteneur vivant
    tail -f /dev/null
}

# Gestion des signaux pour arrêt propre
trap 'echo "🛑 Arrêt de GMS..."; exit 0' SIGTERM SIGINT

# Exécution
main