# Yacht-Like Dev Handoff

## 1) Current Goal
- Build the Yacht Dice battle scene for Steam (PC).
- Core loop currently targeted:
  - Spawn and align 5 dice
  - Click-to-lock dice
  - Max 2 rerolls per turn
  - Confirm score -> end turn -> next turn

## 2) Current Script Architecture

### `Assets/Scripts/TurnManager.cs`
Role: turn flow and rule control.
- `totalTurns = 12`
- `ReRollsPerTurn = 2`
- Handles Space input for reroll attempts
- `ConfirmScore(string scoretype)`:
  - Reads current dice values
  - Calculates score via `ScoreManager`
  - Records score in `ScoreSheet`
  - Updates one score slot UI
  - Calls `EndTurn()` on successful record

References:
- `DiceRoller diceRoller`
- `ScoreManager scoreManager`
- `UIManager uiManager`

### `Assets/Scripts/DiceRoller.cs`
Role: dice spawning/rerolling/animation.
- `RollDiceSequence()`: spawn 5 dice
- `ReRollDiceSequence()`: reroll only unlocked dice
- `StartNewTurnSequence()`: clear existing dice/lists, then spawn new turn dice
- `GetCurrentDiceValues()`: returns `List<int>`
- `IsRolling`: lock user actions while dice system is busy
- Uses DOTween `DOMove` for movement animation

Internal state:
- `List<DiceData> diceDataList`
- `List<GameObject> diceGameObjects`

### `Assets/Scripts/DiceController.cs`
Role: per-die click lock toggle.
- `OnMouseDown()` -> `ToggleLock()`
- Toggles `diceData.isLocked`

### `Assets/Scripts/DiceData.cs`
Role: pure data object.
- `int value`
- `bool isLocked`
- Constructors initialize values

### `Assets/Scripts/ScoreManager.cs`
Role: score calculation logic.
- `CalculateScore(List<int> diceValues, string scoreType)`
- Categories implemented:
  - Ones ~ Sixes
  - FourOfAKind, FullHouse, SmallStraight, LargeStraight, Yacht, Chance
- `ThreeOfAKind` removed

### `Assets/Scripts/ScoreSheet.cs`
Role: score record state holder.
- Stores category scores
- `RecordScore(...)` prevents duplicate record
- `GetUpperSum()`, `GetBonus()`, `GetTotalScore()`
- `IsScoreSheetComplete()`

### `Assets/Scripts/UIManager.cs`
Role: score slot text mapping and updates.
- `UpdateScoreSlot(string scoreType, int score)`
- Maps `scoreType` to corresponding TMP text field via switch
- Summary/turn info update methods not yet implemented

### `Assets/Scripts/GameManager.cs`
- Currently unused (empty placeholder)

## 3) What Is Already Working
- Turn start clears and respawns 5 dice
- Reroll limit per turn (2)
- Locked dice excluded from reroll
- Score confirmation path:
  - calculate -> record -> update slot text -> end turn
- Turn progression up to 12 turns
- Logs `Game Over!` when turns exceed limit

## 4) Fixed Rule Decisions
- Platform target: Steam (PC)
- Turns: 12
- `ThreeOfAKind` category removed

## 5) Next Priorities

### P1. Extend UI updates beyond single score slot
Implement in `UIManager`:
- `UpdateTurnInfo(currentTurn, rerollsLeft)`
- `UpdateSummary(upperSum, bonus, totalScore)`

Call from `TurnManager`:
- after `StartTurn()`
- after `TryReRoll()`
- after successful `ConfirmScore()`

### P2. Final validate all 12 score button bindings
Check string keys in UI OnClick:
- `Ones`, `Twos`, `Threes`, `Fours`, `Fives`, `Sixes`
- `FourOfAKind`, `FullHouse`, `SmallStraight`, `LargeStraight`, `Yacht`, `Chance`

### P3. Add augment system
Planned first augment:
- Name: "God's Intention"
- Trigger: when rerolls are fully depleted
- Effect: reroll one random unlocked die

Recommended architecture:
- `TurnManager`: emit timing/event hooks
- `AugmentManager`: decide and execute augment effects
- `DiceRoller`: provide concrete action method (ex: reroll random unlocked die)

## 6) Known Risks / TODOs
- `DiceController.ToggleLock()` has no null check for `diceData` (possible NRE if not wired)
- `UIManager` currently updates only category slot texts
- String-based score keys are typo-prone (consider enum later)

## 7) Quick Regression Test Checklist
1. Start turn -> 5 dice appear
2. Lock 2 dice
3. Reroll twice -> only unlocked dice reroll
4. Third reroll attempt blocked
5. Confirm one score category -> text updates
6. Re-confirm same category blocked
7. Turn advances and ends at turn 12 with game-over log

