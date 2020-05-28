(*  driver program for the DTM (Do The Math) language that takes input from the user, parses it and interprets it using 
    the appropriate library calls, and displays the result. *)

open System
open ProjectInterpreter

[<EntryPoint>]
let main argv =
    // usage statement to ensure proper input is passed into the program
    if argv.Length <> 1 then
        printfn "ERROR: Input expected, but was not received. \nUsage: dotnet run <program> \nExample: dotnet run \"(+ 1 2 3)\""
        exit 1

    // input made by the user
    let input = argv.[0]

    // checks if file being passed in or direct input on the command line
    if ((input.[(input.Length-4)..(input.Length-1)]) = ".dtm")
    then
        // changes the string from seq<string> to string
        let new_input = readLines input |> Seq.toArray

        // calls the driver method to evaluate the input from file
        driver new_input.[0] new_input.[0]

    else
        // calls the driver method to evaluate the input
        driver argv.[0] input