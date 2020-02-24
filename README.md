# Sudoku-Solver
A Sudoku Solver that incorporates C#, WPF, the Tesseract.Net SDK, and an implementation of the backtracking algorithm.
![Sudoku Cover Image](https://github.com/ismaelvazquezjr/Sudoku-Solver/blob/master/Sudoku-Solver.jpg)

This program is written in C#, while the GUI aspect was created using the Windows Presentation Foundation(WPF) framework. I attempted to use color's that complimented each other and created a calming/soothing effect for the user.

![Screenshot of the Sudoku Solver](https://github.com/ismaelvazquezjr/Sudoku-Solver/blob/master/program_screenshot.png)

The icon(ico) image was taken from IconFinder by searching for free to use "cubes." The button fonts are using the Kenyan Coffee fonts, while the grid values are using a font called Alex's Writing. The goal was to mix professionalism with playfulness. 

![Screenshot of the Sudoku Solver with values](https://github.com/ismaelvazquezjr/Sudoku-Solver/blob/master/program_screenshots_with_values.png)

Scanning values in from an image requires an external dependency which is called the Tesseract.net OCR SDK. You're limited to only scanning 500px by 500px image files without a commercial license. This was perfecty fine for my needs but it's something to consider if you have different requirements.

Tesseract scans the text from the image as a single string and it removes any spacing between the numbers. In order to make use of the "Scan Image" functionality, you'll need to manually insert X's into the blank spaces. You will also need to remove the borders from the board.

![Image of the required image format](https://github.com/ismaelvazquezjr/Sudoku-Solver/blob/master/newboard.jpg)

A final note, if you need the full functionality of this program, downloading just the standalone binary is not enough. You will also need to download file Patagames.Ocr.dll file and the x64,x86, and Tessdata folders with their respective contents.

To notify the user which values the program inserted into the board, those values have been marked in red to differentiate from the default values.

![Screenshot of solved sudoku puzzle](https://github.com/ismaelvazquezjr/Sudoku-Solver/blob/master/solved_screenshot.png)

To actually solve the puzzle, the program implements a backtracking algorithm. Backtracking is a general algorithm for finding all solutions to some computational problems, notably constraint satisfaction problems, that incrementally builds candidates to the solutions, and abandons a candidate as soon as it determines that the candidate cannot possibly be completed to a valid solution.

Here's a video demonstration(YouTube):

[![Youtube Link to demo](http://img.youtube.com/vi/pw6w8KFaC-k/0.jpg)](http://www.youtube.com/watch?v=pw6w8KFaC-k)
