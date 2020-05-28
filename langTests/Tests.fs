namespace langTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Parser
open ProjectParser
open ProjectInterpreter

[<TestClass>]
type TestClass () =

    (* PARSER TESTS, CHECKS ON ProjectParser *)

    // tests to check that an integer is returned correctly
    [<TestMethod>]
    member this.ValidNumberIsReturnedAgain() =
        let input = "1"
        let expected = Num 1.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a negative integer is returned correctly
    [<TestMethod>]
    member this.ValidNegNumberIsReturnedAgain() =
        let input = "-1"
        let expected = Num -1.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a float is returned correctly
    [<TestMethod>]
    member this.ValidDecimalIsReturnedAgain() =
        let input = "1.1"
        let expected = Deci 1.1
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a negative float is returned correctly
    [<TestMethod>]
    member this.ValidNegDecimalIsReturnedAgain() =
        let input = "-1.1"
        let expected = Deci -1.1
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    (* PREFIX PARSERS *)

    // tests to check that a prefix plus expression is returned correctly
    [<TestMethod>]
    member this.ValidPrePlusExprIsReturnedAgain() =
        let input = "(+ 1 2)"
        let expected = Pre_Plus [Num 1.0; Num 2.0]
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a prefix subtraction expression is returned correctly
    [<TestMethod>]
    member this.ValidPreSubExprIsReturnedAgain() =
        let input = "(- 1 2)"
        let expected = Pre_Sub [Num 1.0; Num 2.0]
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a prefix subtraction expression with a negative number is returned correctly
    [<TestMethod>]
    member this.ValidNegPreSubExprIsReturnedAgain() =
        let input = "(- 1 -2)"
        let expected = Pre_Sub [Num 1.0; Num -2.0]
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a prefix multiplication expression is returned correctly
    [<TestMethod>]
    member this.ValidPreMultExprIsReturnedAgain() =
        let input = "(* 1 2)"
        let expected = Pre_Mult [Num 1.0; Num 2.0]
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a prefix division expression is returned correctly
    [<TestMethod>]
    member this.ValidPreDivExprIsReturnedAgain() =
        let input = "(/ 1 2)"
        let expected =Pre_Div [Num 1.0; Num 2.0]
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a prefix exponentiation expression is returned correctly
    [<TestMethod>]
    member this.ValidPreExpoExprIsReturnedAgain() =
        let input = "(^ 1 2)"
        let expected = Pre_Expo [Num 1.0; Num 2.0]
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    (* INFIX PARSERS *)

    // tests to check that a infix plus expression is returned correctly
    [<TestMethod>]
    member this.ValidInPlusExprIsReturnedAgain() =
        let input = "(1 + 2)"
        let expected = In_Plus (Num 1.0,Num 2.0)
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a infix subtraction expression is returned correctly
    [<TestMethod>]
    member this.ValidInSubExprIsReturnedAgain() =
        let input = "(1 - 2)"
        let expected = In_Sub (Num 1.0,Num 2.0)
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a infix subtraction expression with a negative number is returned correctly
    [<TestMethod>]
    member this.ValidNegInSubExprIsReturnedAgain() =
        let input = "(1 - -2)"
        let expected = In_Sub (Num 1.0,Num -2.0)
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a infix multiplication expression is returned correctly
    [<TestMethod>]
    member this.ValidInMultExprIsReturnedAgain() =
        let input = "(1 * 2)"
        let expected = In_Mult (Num 1.0,Num 2.0)
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a infix division expression is returned correctly
    [<TestMethod>]
    member this.ValidInDivExprIsReturnedAgain() =
        let input = "(1 / 2)"
        let expected = In_Div (Num 1.0,Num 2.0)
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a infix exponentiation expression is returned correctly
    [<TestMethod>]
    member this.ValidInExpoExprIsReturnedAgain() =
        let input = "(1 ^ 2)"
        let expected = In_Expo (Num 1.0,Num 2.0)
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, res)
        | Failure(_, _) -> Assert.IsTrue false

    (* EVALUATOR TESTS, CHECKS ON ProjectInterpreter *)
    
    // tests to check that an integer is returned correctly
    [<TestMethod>]
    member this.ValidNumberIsEvaledCorrectly() =
        let input = "1"
        let expected = 1.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a negative integer is returned correctly
    [<TestMethod>]
    member this.ValidNegNumberIsEvaledCorrectly() =
        let input = "-1"
        let expected = -1.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a float is returned correctly
    [<TestMethod>]
    member this.ValidDecimalIsEvaledCorrectly() =
        let input = "1.1"
        let expected = 1.1
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a negative float is returned correctly
    [<TestMethod>]
    member this.ValidNegDecimalIsEvaledCorrectly() =
        let input = "-1.1"
        let expected = -1.1
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    (* PREFIX EVALUATIONS *)

    // tests to check that a prefix plus expression is returned correctly
    [<TestMethod>]
    member this.ValidPrePlusExprIsEvaledCorrectly() =
        let input = "(+ 1 2)"
        let expected = 3.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a prefix subtraction expression is returned correctly
    [<TestMethod>]
    member this.ValidPreSubExprIsEvaledCorrectly() =
        let input = "(- 1 2)"
        let expected = -1.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

     // tests to check that a prefix subtraction expression with a negative number is returned correctly
     [<TestMethod>]
     member this.ValidNegPreSubExprIsEvaledCorrectly() =
         let input = "(- -1 2)"
         let expected = -3.0
         let result = grammar (prepare input)
         match result with
         | Success(res, _) -> Assert.AreEqual(expected, eval res)
         | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a prefix multiplication expression is returned correctly
    [<TestMethod>]
    member this.ValidPreMultExprIsEvaledCorrectly() =
        let input = "(* 1 2)"
        let expected = 2.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a prefix division expression is returned correctly
    [<TestMethod>]
    member this.ValidPreDivExprIsEvaledCorrectly() =
        let input = "(/ 1 2)"
        let expected = 0.5
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a prefix exponentiation expression is returned correctly
    [<TestMethod>]
    member this.ValidPreExpoExprIsEvaledCorrectly() =
        let input = "(^ 1 2)"
        let expected = 1.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    (* INFIX EVALUATIONS *)

    // tests to check that a infix plus expression is returned correctly
    [<TestMethod>]
    member this.ValidInPlusExprIsEvaledCorrectly() =
        let input = "(1 + 2)"
        let expected = 3.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a infix subtraction expression is returned correctly
    [<TestMethod>]
    member this.ValidInSubExprIsEvaledCorrectly() =
        let input = "(1 - 2)"
        let expected = -1.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

     // tests to check that a infix subtraction expression with a negative number is returned correctly
     [<TestMethod>]
     member this.ValidNegInSubExprIsEvaledCorrectly() =
         let input = "(-1 - 2)"
         let expected = -3.0
         let result = grammar (prepare input)
         match result with
         | Success(res, _) -> Assert.AreEqual(expected, eval res)
         | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a infix multiplication expression is returned correctly
    [<TestMethod>]
    member this.ValidInMultExprIsEvaledCorrectly() =
        let input = "(1 * 2)"
        let expected = 2.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a infix division expression is returned correctly
    [<TestMethod>]
    member this.ValidInDivExprIsEvaledCorrectly() =
        let input = "(1 / 2)"
        let expected = 0.5
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false

    // tests to check that a infix exponentiation expression is returned correctly
    [<TestMethod>]
    member this.ValidInExpoExprIsEvaledCorrectly() =
        let input = "(1 ^ 2)"
        let expected = 1.0
        let result = grammar (prepare input)
        match result with
        | Success(res, _) -> Assert.AreEqual(expected, eval res)
        | Failure(_, _) -> Assert.IsTrue false