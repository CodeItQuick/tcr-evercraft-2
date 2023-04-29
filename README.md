# EVERCRAFT Kata with TCR

This is the evercraft Kata developed purely using TCR. The most notable effect of TCR is a lot of frequent
committing, as can be seen by the relatively few hours and high commit numbers in this repository.

# What is TCR?

TCR is test-commit-revert originally coined by Kent Beck. Basically, the tests are run on every code change,
if the tests fail a git revert is run to wipe the work done since the last commit. If the tests pass then
the current work in progress is committed.

# What is Evercraft
See the Evercraft.md file for more information on the details

# Advantages/Disadvantages of TCR

TCR Seems to Encourage:
1. Small Steps - Encouraging writing a small amount of production code at a time
2. Frequent Committing
3. Frequently adding tests
4. More care in what you're doing
5. Velocity/a lot of work seems to get outputted at high quality over short periods of time

TCR Also has some pitfalls:
1. There's a lot of "cheating" going on - initiating running the tests to get code in there,
the manner the tests are written so that they always pass, etc.
2. Encourages developers to be more rigid in their approach to developing
3. Strongly discourages regression in the code


# Requirements Complete:
* [ ] Core (First Iteration)
* [x] Create Character - Character has name, can edit name, can add/remove
* [ ] Alignment
* [x] Armor & HitPoints
* [x] Character Can Attack
* [ ] Character Can Be Damaged
* [ ] Character Has Ability Scores
* [ ] Character Has Ability Modifiers
* [ ] Character Can Gain Experience
* [ ] Character Can Level
