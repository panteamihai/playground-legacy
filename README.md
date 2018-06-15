# Legacy Code Workshop

### Concept: a [JBrains clone] implemented as a workshop*
Invite a group of programmers together for a **one hour and a half** workshop every week, give them some legacy code, and help them practice various design rescue and improvement techniques.
The ultimate goal is to practice these techniques in a low-stakes environment away from the code base that probably torments programmers at work.

\* [do try to favor the retreat flavor if possible].

### The refactoring project
We're going to be using the **[Trivia Game]** (solely the C# flavor) and incrementally try to improve it using refactoring techniques

[Trivia Game]: <https://github.com/caradojo/trivia/tree/master/C%23>
[JBrains clone]: <https://www.jbrains.ca/legacy-code-retreat/>
[do try to favor the retreat flavor if possible]: <https://blog.adrianbolboaca.ro/2014/04/legacy-coderetreat/>

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
* Try to see how to draw out the effects of the `Roll` method.
* Reason about the domain of a game (use Jeopardy for example) and see if the properties/methods you're thinking about exposing make for good characterization properties from a domain perspective.

### Part Six - DIPing

* Inverse the hard dependency on the questions by introducing a `QuestionsGenerator`
* The generator will eventually be fed into a `QuestionsProvider` hopefully, there by abstracting the whole category / question part of the game
* We'll feel the burden of having introduced an enum for the category

### Part Seven - Pushing the DIP forward

* Having introduced the `ICategoryProvider` and fixed the dependecy hierarchy of the `IQuestionProvider` (specifically the dependency of `IQuestionGenerator` implementation to an `ICategoryProvider` implementation, handled by the `Game` class)
* Moving on to extracting a service for player location and sensing the other responsabilities of the current `Game` class ripe for an  (read + write = service) extraction to robust collaborators: the player coin standing, the current player information, the penalty box.

### Part Eight - Consolidating the service
* Introduce a Player class to encapsulate the domain characteristics of a player: name and ordinal at first. Then start to incorporate the location, and subsequently the coin standing and the penalty box relationship.
* Transfer the game logic to the player service in steps related to functionality: location, coin standing, penalty status.

### Part Nine - Consolidated player service
* While implementing the `Penalty` service, a hypothetical bug in the current system emerged: once in the penalty box, even though the player can roll a dice that would make it so that he/she is flagged with a "getting out of the penalty box" status, the actual removal from the penalty box never occurs. Now, since this refactoring exercise should not change existing business/domain rules, we chose to move forward with a new vision over the penalty box functionality: you can move burden free until a penalty is incurred, after which the player's odds get trimmed down to a 1 in 2 of actually moving (based on the roll outcome).
* The player service is now a facade for the `Location` service, the `CoinBalance` service and the `Penalty` service. The `Player` class is comprised of a read-only, i.e. immutable, part (the name and ordinal) and a mutable status part(location, coins, penalty status). A discussion over a further split can be started.

### Part Ten - Wrapping up
* Talk about the current code coverage, statements vs. branches. Remove the golden master and redo the coverage, spot the differences.
* Discuss the most important lessons learnt: impact of sensing variables on getting things going, the importance of an effect sketch analysis on getting the bigger picture, how characterization tests for you to think about your app's API at a higher level (because implementation details are definitely subject to change)
* A mental exercise about implementing this whole system in a reactive way driven by the fact that there are readily identifiable streams of events (which can be Zip-ed into one) that determine the progression of the game (Scan) up to its conclusion (TakeUntil).

# Attention Note
The golden master can be generated by checking out the 497c9f02f47dcfe2a057ce42f364fe09734a7ff1 revision and running the `GenerateInput`/`GenerateGoldenMaster` methods. This will output a **Output.txt** file that represents the golden master. However in the fa68c635c71e8939858fa98cf0c6a2274ab59bdf revision, the name of the file was changed to **GoldenMaster.txt**, so if you plan on running the tests post-fa68c635c71e8939858fa98cf0c6a2274ab59bdf please remember to manually rename this file. I know this was not the best forward-thinking decision ever made.