# Foivos Design Notes
## Καρτες
Θα υπάρχουν 2 ειδών κάρτες: summons και effects.
### Summons
Δες [εδω](#entities) για περιγραφή των allies που θα γίνονται summon. 
### Effecs
Υπάρχουν πολλές κάρτες που μπορούν να σχεδιαστούν. Γενικά προτιμώ effects που να έχουν συναργασίες μεταξύ τους. Για να υπάρχει συνεργασία προτέινω να υπάρχουν διαφορετικά damage types:

- Physical
- Fire
- Thunder
- Frost
- Earth

Επίσης θα υπάρχουν διάφορα status conditions:

- Wet: Διπλό damage απο thunder, μισό damage απο fire, με damage απο frost το wet γίνεται frozen. Μαζί με electrified γινεται stunned.
- Frozen: Ουσιαστικά stunned. Τρώει διπλο damage απο fire και τελειώνει.
- Poisoned: Damage over time. Τρώει διπλο damage απο earth. Τελεώνει με heal.
- Electrified: Τρώει διπλο damage απο thunder και τελειώνει. Μαζί με wet γινεται stunned.
- Bleeding: Damage over time. Τελεώνει με heal.
- Slowed: Μισό speed και attack speed.
- Hasted: +50% speed και attack speed.
- Restricted: Movement speed μηδέν.
- Confused: Επιτίθεται σε όλους.
- Mind Control: Ενας εχθρος πολεμάει για σενα.
- Immunities: Κάθε entitiy μπορεί να έχει immunities σε ένα ή περισσότερα status (τα boss ας πούμε θα είναι immune στο mind control μάλλον).
- Knockback: Όχι ακριβώς status αλλα θα υπάρχουν effects που θα κάνουν knockback.

## Entities
Θα υπάρχουν 3 ειδών entities: attacking, charging και passive. Αναλογα το speed μπορεί μερικά απο αυτά ναι είναι και towers (ακίνιτα).

### Attacking
Αυτα τα entities θα βρίσκουν ενα target και θα κανουν ενα effect σε αυτον τον εχθρό.
### Charging
Αυτα τα entities θα κανουν ένα effect καθε τόσο ανεξάρτητα απο target (είτε AoE είτε projectiles). Μερικά τέτεια entities θα κάνουν και summon αλλα entities.
### Passive
Αυτα τα entities θα παρέχουν ένα buff/debuff σε πολλά entities.

## Specific Designs
### Cards
Name | Mana Cost | Target | Effect
--- | --- | --- | --- 
Dart| 2 | Projectile | Deal 3 physcal damage
Poison Dart | 2 |  Projectile | Inflict poison that deals 1/s for 5s
Rain | 1 | 2x2 Square | Inflicts wet
Thunderclap | 4 | 1x1 Square | Deal 2 thunder damage 
Boulder | 5 | Piercing Projectile | Deal 2 damage
Frostbolt | 2 | Projetile | Deal 2 frost damage
Firebolt | 2 | Projetile | Deal 2 fire damage
Fireball | 6 | 2x2 Square | square deal 3 fire damage
Chain Lighting | 5 | Target, Chains 2 | Deals 3 thunder damage and inflict electrified 
Net | 3 | 2x1 Square | Restrict all enemies for 4s
Slowing Field | 3 | Turret |  Slow all enemies in a lane
Arrow Turret | 3 | Turret | Ranged physical attack 1/s
Blast Turret | 5| Turret | Deal fire damage periodically around it
Conductor Coil | 3 | Turret | All enemies in its lane are shocked
Toxic Fumes | 6 | Turret | All enemies in its lane are posioned for 0.5/s

### Enemies
Name | Health | MS | AS | Attack 
--- | --- | --- | --- |--- 
Goblin Warrior | 5 | 90 | 1 | Deal 2 Damage 
Goblin Spear | 3 | 90 | 1 | Deal 2 Damage 
Goblin Witch | 6 | 0 | 1 | All goblins in its lane are hasted
Goblin Siege Engine | 15 | 40 | 0.5 | Deal 4 Damage, only targets turrets
Goblin Summoner | 5 | 0 | 0.5 | Summon a goblin

