# Nim-Game
This was created as a group project in a C# class.
I created the class and the main program. My partner created the pile list class.

These are the instructions we were given:

You and your group are going to design and implement a C# console application that plays a (hopefully) winning game of Nim 
against a human opponent. The interface will be just the command line. When the program begins it should present the user 
with a short introduction and then play should commence. It is assumed that both parties know the rules. Here's how the game 
should proceed:

1.	The game asks the user if (s) wants to play first or second.
2.	The game asks the user how many piles to play and the initial number of sticks in each pile.
3.	Play commences.
4.	The first player tells how many sticks to remove from which pile.
5.	The second player tells how many sticks to remove from which pile.
6.	The process repeats until the last stick(s) are removed. The player removing the lasts sticks is the winner.
7.	The game then asks if the user would like to play again. If so, jump to step 1.

You must decide what the prompts and responses will look like. Your program must deal with improper or unexpected responses 
using exception handling wherever appropriate.

Just before it's the player's turn to play you should display the number of sticks in each pile in a form similar to that shown 
above. You probably should number the piles to make things easier for everyone.

You must decide what classes and objects are appropriate in order to create this game. You might want to use the Nodes you 
created in the last assignment -- in a list this time, rather than in a ring. Each list could represent a pile. Each node could 
contain a single bit. The constructor for a pile could initialize the list with the bit-equivalent of the number of sticks in 
the pile.


For information on how to win a game of Nim see here: http://www.archimedes-lab.org/game_nim/nim.html


The logic for the computer's turn is designed to leave a Nim sum of 0.  This always places the computer in the winning position.
