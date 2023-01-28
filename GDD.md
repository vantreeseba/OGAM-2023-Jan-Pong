## GDD
Any words spelt AS_THIS_IS are placeholders to be determined later.

## Basic Elements
There should be a main menu screen, with a start and exit button.

Start should begin the gameplay state.
Exit should exit the program (or whatever if it's on a website.)

## Gameplay elements
#### Play area
----
The game "field" is a 2d space, constrained to the xy plane.

#### Paddles
----
The player(s) control a paddle that can hit a ball.
The paddles are constrained onto the y axis.

The paddles have some defined WIDTH and HEIGHT, the height being greater than width.
The paddles can only move within PADDLE_VERTICAL_RANGE up and down the y axis.

#### Ball
----

The ball begins in the middle of the field, and is given SOME_RANDOM_VELOCITY on X and Y.
When it strikes either the edge of the game field or a paddle, it changes it's velocity as a reflection of the impact.

So if it strikes at 45deg, it should leave with a -45deg direction.

This could be done via inverting the x and y vel respectively.

Can this just use rigid body physics and not have to code any of my own?


#### Scoring
----

The gameplay area should have two "goals" defined, behind each player paddle on the x axis respectively.
If the ball enters the goal area, it should be destroyed, replaced in center, and after a delay, begin moving again.

There should be some state tracking, where player1 and player2 haves scores that are incremented by 1 when the ball enters the goal.

#### Exit State
----
If the player hits SOME_EXIT_KEY the game should return to the main menu.

The game ends when a single player scores WIN_AMOUNT goals.

#### UI Elements
----
At the top of the screen, it should show the current goal amounts, and the current length of the game in seconds?

## Art/UI/Music/SFX
#### UI
----
The main menu icons, buttons and misc can use kenney assets.
The score text can use a default font.

#### Meshes
----
Required meshse are nill, as I can just use built in unity cubes and etc.

#### Textures
----
I can use kenney prototype textures for now, and see if that looks good enough.

#### Music
----
The main menu should have some theme.
The gameplay state should have some different theme(s).

#### Ball
----
On contact with paddle, should play some BALL_PADDLE_SFX.
On contact with wall, should play some BALL_WALL_SFX.
On contact with goal, should play some BALL_GOAL_SFX.
On inital start, should play some BALL_START_SFX.