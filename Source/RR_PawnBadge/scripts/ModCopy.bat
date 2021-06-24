@echo off
robocopy "C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\PawnBadge-FanFork" "C:\Program Files (x86)\Steam\steamapps\common\RimWorld1.2\Mods\PawnBadge-FanFork" /MIR /xd .git Source Ideology 1.3 /xf "*.zip" "*.7z" "*.psd" "*.psb" "*.md" ".gitattributes" ".gitignore" "About.xml" "LoadFolders.xml" "LICENSE"
exit 0