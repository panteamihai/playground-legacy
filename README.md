# Legacy Code Workshop

### Concept: a [JBrains clone] implemented as a workshop*
Invite a group of programmers together for a **one hour and a half** workshop every week, give them some legacy code, and help them practice various design rescue and improvement techniques.
The ultimate goal is to practice these techniques in a low-stakes environment away from the code base that probably torments programmers at work.

\* [even though you're not supposed to].

### The refactoring project
We're going to be using the **[Trivia Game]** (solely the C# flavor) and incrementally try to improve it using refactoring techniques

[Trivia Game]: <https://github.com/caradojo/trivia/tree/master/C%23>
[JBrains clone]: <https://www.jbrains.ca/legacy-code-retreat/>
[even though you're not supposed to]: <https://blog.adrianbolboaca.ro/2014/04/legacy-coderetreat/>

### Part One - Implementing a golden master

* Study the output of the sample game runner.
* Bring the random generator under a test harness.
* Think long and hard about what input will exercise the whole system as much as possible.
* Implement an input generator.
* Implement a golden master generator to be used once and only once hopefully.

PS: It's nice to have a [guide].

[guide]: <https://code.tutsplus.com/tutorials/refactoring-legacy-code-part-1-the-golden-master--cms-20331>

### Part Two - Some clean-up

* Study the general appearence of the code inside the class.
* Use automated tools (Resharper & CodeMaid) to perform bulk side-effect free refactorings.
* Try your hands at some side-effect free manual refactorings like. method extraction.

### Part Three - Perform an analysis

* Put your whiteboard markers to work and draw the [effect sketches] for the system.
* Alternatively, use something like graphviz to do the above digitally.
* Reason about good pinch/interception points and good points to exercise the system under a test suite.

[effect sketches]: <https://gist.github.com/jeremy-w/6986692#chapter-11-i-need-to-make-a-change-what-methods-should-i-test>

### Part Four - Start bringing the system under test

* Look into ways that the current system can be brought under tests, in as simple a way as possible.
* Reason about capturing console output for testing purposes going forward or alternatively exposing sensing variables (that make sense from a domain perspective).
* Start adding characterization tests for edge cases and move forward.

### Part Five - Build up the test suite

* Finish the unplayable game test cases and start the playable game (interesting) ones.
* Try to see how to draw out the effects of the Roll method.
* Reason about the domain of a game (use Jeopardy for example) and see if the properties/methods you're thinking about exposing make for good characterization properties from a domain perspective.