# EVERCRAFT Kata with TCR

This is the evercraft Kata developed purely using TCR. The most notable effect of TCR is a lot of frequent
committing, as can be seen by the relatively few hours and high commit numbers in this repository. The first
iteration of this kata is completed, admittedly with some very unique rules implemented. These are:

1. Characters that have a lower strength get hit harder.
2. When attacking, the highest character level present gets added to the damage given.

These rules were somewhat implemented as a result of TCR. It would have been difficult to refactor the code after
I had figured out these bugs were in there/the code was slightly wrong from a requirements point of view.
I can still probably go through the refactor later, but put it off to "finish" the project.

# What is TCR?

TCR is test-commit-revert originally coined by Kent Beck. Basically, the tests are run on every code change,
if the tests fail a git revert is run to wipe the work done since the last commit. If the tests pass then
the current work in progress is committed.

# What is Evercraft
See the Evercraft.md file for more information on the details

# Advantages/Disadvantages of TCR

Biggest Takeaway:
Should be running tests very frequently, on the order of every 2-3 minutes causes a huge improvement
in code quality/work flow.


TCR Seems to Encourage:
1. Small Steps - Encouraging writing a small amount of production code at a time
2. Frequent Committing
3. Frequently adding tests
4. More care in what you're doing
5. Velocity/a lot of work seems to get outputted

TCR Also has some pitfalls:
1. There's a lot of "cheating" going on - initiating running the tests to get code in there,
the manner the tests are written so that they always pass, etc.
2. Encourages developers to be more rigid in their approach to developing
3. Strongly discourages regression in the code

In retrospect this method did result in less incentive to refactor, and therefore ugly code. For that
reason I probably wouldn't do it in a production environment.


# Requirements Complete:
* [x] Core (First Iteration)
* [x] Create Character - Character has name, can edit name, can add/remove
* [x] Alignment
* [x] Armor & HitPoints
* [x] Character Can Attack
* [x] Character Can Be Damaged
* [x] Character Has Ability Scores
* [x] Character Has Ability Modifiers
* [x] Character Can Gain Experience
* [x] Character Can Level
