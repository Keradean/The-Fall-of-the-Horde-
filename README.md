# The Fall of the Horde!

Ein 3D Tower Defense in mittelalterlicher Fantasy Welt. Abgabeprojekt für die Praktische Abschlussarbeit 2026 an den SRH Fachschulen.

**Autor:** Dennis De Col · **Gruppe:** GME 24.01 · **Engine:** Unity 6000.3.10f1 · **USK:** 12

---

## Worum geht's

Du verteidigst eine Burg gegen Wellen anrückender Orks, Goblins und einem fliegenden Magier. Platziere Türme, verdiene Gold durch besiegte Gegner, upgrade was du hast oder verkaufe es für 60 % zurück wenn's nicht passt. Wenn die Burg fällt, gibt's Game Over, mit dem zweifelhaften Bonus, dass die Horde einen kleinen Siegestanz aufführt.

Es gibt drei Karten mit steigender Schwierigkeit: Level 1 hat einen Pfad, Level 2 zwei, Level 3 drei.

## Projekt öffnen

Du brauchst:

- Unity Hub
- Unity Editor **6000.3.10f1** 

Dann:

1. Projekt klonen
2. In Unity Hub über *Add* den Projektordner hinzufügen und öffnen
3. **Szene `Assets/Scenes/MainMenu.unity` öffnen** 
4. Play drücken

## Steuerung

Linksklick auf einen Tower-Button wählt einen Turm aus, Linksklick aufs Spielfeld platziert ihn (grün = geht, rot = blockiert). Klick auf einen bereits platzierten Turm öffnet das Upgrade-/Verkaufsmenü. ESC pausiert das Spiel bzw. bricht eine laufende Platzierung ab. Über die UI Buttons kannst du die Spielgeschwindigkeit auf langsam, normal oder schneller stellen.

## Türme

| Turm | Typ | Rolle |
|---|---|---|
| Ballista | Projektil, Einzelziel | Allrounder |
| Cannon Tower | Projektil, schwer | Hoher Einzelziel Schaden |
| Fire Tower | Projektil + DoT | Entflammt Gegner für Schaden über Zeit |
| Ice Tower | AoE-Aura | Verlangsamt alles im Radius |

Jeder Turm lässt sich einmal upgraden (Level 1 → Level 2).

## Gegner

Es sind deutlich mehr als die geforderten drei, ich wollte pro Level ein bisschen Abwechslung haben:

- **Goblin** – Scout, schnell, wenig HP
- **Orc Swordmann** und **Orc Swordmann Level 2** – Standard Nahkampf in zwei Stärken
- **Orc Axemann** und **Orc Axemann Level 2** – Tanks mit viel HP
- **Element** – fliegt, kann nur von Luftabwehr Türmen getroffen werden
- **Orc Boss Axemann** – Mini Boss
- **Orc King** – Endboss

## Level

- **Level One** (1 Pfad, Einstieg)
- **Level Two** (2 Pfade, mittel)
- **Level Three** (3 Pfade, Endkampf)

Alle drei sind über das Level Select Menü direkt anwählbar.

## Third-Party Assets

Alle fremden Medien sind im separaten **Medienkatalog** (`Konzeption/Medienkatalog.pdf`) im Detail aufgeführt. Kurz zusammengefasst:

- **Kenney.nl** (CC0) – Tower Defense Kit, Mini Dungeon, Fantasy UI Borders, Impact Sounds
- **Unity Technologies** – Particle Pack (Fireball-VFX) über den Asset Store
- **Mixamo (Adobe)** – Charakter Animationen für alle Gegner (Walk, Attack, Death, Dance)
- **OpenGameArt.org** (CC0) – 9 Musik-Tracks und mehrere Sound-Packs

### Zur KI-Nutzung

Ich habe Claude genutzt, um Unity Fehlermeldungen zu verstehen und bei der Strukturierung der Abgabedokumente. Nichts davon ist KI generiert, weder Code, noch Modelle, Texturen, Musik, Sounds oder das Spielkonzept selbst. Konkrete Prompts und Details stehen im Medienkatalog.

## Wie die Vorgaben umgesetzt sind

| Vorgabe | Wo / wie |
|---|---|
| Automatische Geschütztürme | `ProjectileTower.cs` zielt und schießt autonom, `SlowdownTower.cs` arbeitet über einen AoE-Trigger |
| Gegner laufen Pfad zur Basis | Waypoint-System in `Path.cs`, Bewegungslogik in `Enemy.cs` → `MoveAndAttack()` |
| Ressource begrenzt Türme | `GoldManager.cs` mit `SpendGold()` / `AddGold()` |
| UI zeigt Wellen, Leben, Geld | `UIController.cs` – HUD mit Gold-Anzeige, Welle (X/Y), Burg-Healthbar |
| Mindestens 3 Karten | drei Level |
| Mindestens 3 Türme | vier Türme |
| Mindestens 3 Gegnertypen | acht Gegnertypen |
| Skalierbarkeit | ScriptableObjects für Stats, Polymorphie, Prefabs, Object Pooling |

## Ein bisschen was zum Code

Ich habe mir beim Aufbau bewusst Mühe mit der Architektur gegeben, weil das ja mitbewertet wird. Das Wichtigste:

- Generisches Singleton (`Extra/Singleton<T>.cs`) mit einstellbarem `PersistAcrossScenes` – nutzen alle Manager (Gold, Tower, Pool, Level, Audio)
- `IDamageable` Interface für alle schadensnehmbaren Objekte
- Stats Vererbung: `TowerStats` → `ProjectileStats` → `FireTowerStats` / `CastleDefenceStats`. Der Ice Tower läuft über einen eigenen Zweig (`SlowDownTowerStats`)
- Object Pooling für Projektile und für Gegner pro Welle, kein Garbage-Collection-Spike während der Wellen
- `Physics.OverlapSphereNonAlloc` mit vorallokierten Arrays in der Enemy Detection, und die Detection läuft nur alle 200 ms per Coroutine statt jedes Frame
- Alle Balancing Werte liegen als ScriptableObjects im Editor, keine Hardcoded Values im Code

## Features die nicht Pflicht waren

Nur weil's ein bisschen Liebe verdient hat:

- Range Indicator beim Turm platzieren mit grün/rot- Feedback für gültig/ungültig
- Upgrade Fenster mit Live 3D Preview des Turms
- Verkaufssystem mit 60 % Gold Refund
- Zeitsteuerung Pause / langsam / normal / schnell
- Fliegende Gegner, die eine Luftabwehr Strategie erzwingen 
- Die Burg hat mehrere Angriffspunkte und jeder Gegner wählt beim Spawn zufällig einen aus
- Burn Damage over Time (Fire Tower) und Slow Aura (Ice Tower) als eigene Systeme
- Billboarding Healthbars
- Automatische BGM Weiterschaltung durch die Track Liste
- Und wenn die Burg fällt, tanzt die Horde

## Szenen im Projekt

- `Assets/Scenes/MainMenu.unity`  **Start-Szene**
- `Assets/Scenes/LevelSelect.unity`
- `Assets/Scenes/LevelOne.unity`
- `Assets/Scenes/LevelTwo.unity`
- `Assets/Scenes/LevelThree.unity`

## Scripts Struktur

```
Assets/Scripts/
├── Manager/      GoldManager, TowerManager, PoolManager, LevelManager, AudioManager
├── UI/           UIController, TowerButton
├── Tower/        Tower, ProjectileTower, SlowdownTower, UpgradeTower, TowerStats/...
├── Enemy/        Enemy, EnemyHealth, EnemyStats
├── Projectile/   Projectile (mit Pool), ProjectileStats
├── Path/         Waypoint-System
├── Castle/       CastleHealth, CastleStats
├── MainMenu/     MainMenu, LevelSelect
└── Extra/        Singleton<T>, IDamageable, Spawner, WaveStats
```

---

*The Fall of the Horde! – Praktische Abschlussarbeit 2026, SRH Fachschulen GmbH*
