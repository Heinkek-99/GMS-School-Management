# backup-database.ps1
$date = Get-Date -Format "yyyyMMdd_HHmmss"
$backupPath = "C:\GMS\Backups\GmsDb_$date.bak"

sqlcmd -S (localdb)\mssqllocaldb -Q "BACKUP DATABASE GmsDb TO DISK='$backupPath'"

Write-Host "✅ Backup créé : $backupPath" -ForegroundColor Green