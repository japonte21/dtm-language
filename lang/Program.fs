(*  driver program for the DTM (Do The Math) language that takes input from the user, parses it and interprets it using 
    the appropriate library calls, and displays the result. *)

open System
open Parser
open ProjectParser
open ProjectInterpreter

[<EntryPoint>]
let main argv =
    // usage statement to ensure proper input is passed into the program
    if argv.Length <> 1 then
        printfn "ERROR: Input expected, but was not received. 
                 Usage: dotnet run <program> 
                 Example: dotnet run \"(+ 1 2 3)\""
        exit 1

    // parses and evaluates user input
    let input = prepare argv.[0]
    match grammar input with
    | Success(res,_) ->
        printfn "%A" (eval res)
        0
    | Failure _ ->
        printfn "Failure!"
        1