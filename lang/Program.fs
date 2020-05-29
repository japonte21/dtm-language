(*  driver program for the DTM (Do The Math) language that takes input from the user, parses it and interprets it using 
    the appropriate library calls, and displays the result. *)

open System
open ProjectInterpreter

[<EntryPoint>]
let main argv =
    // no input, calls the REPL for the Program and provides Usage statement
    if argv.Length <> 1 then

        // kind of obnoxious in the code, but looks nice for the REPL
        printfn " .----------------.  .----------------.  .----------------. "
        printfn "| .--------------. || .--------------. || .--------------. |"
        printfn "| |  ________    | || |  _________   | || | ____    ____ | |"
        printfn "| | |_   ___ `.  | || | |  _   _  |  | || ||_   \\  /   _|| |"
        printfn "| |   | |   `. \\ | || | |_/ | | \\_|  | || |  |   \\/   |  | |"
        printfn "| |   | |    | | | || |     | |      | || |  | |\\  /| |  | |"
        printfn "| |  _| |___.' / | || |    _| |_     | || | _| |_\\/_| |_ | |"
        printfn "| | |________.'  | || |   |_____|    | || ||_____||_____|| |"
        printfn "| |              | || |              | || |              | |"
        printfn "| '--------------' || '--------------' || '--------------' |"
        printfn " '----------------'  '----------------'  '----------------' "
        printfn "\nWelcome to DTM!"
        printfn "Enter an expression to be evaluated, else type \"quit\" to exit."
        printfn "Note: If you're attempting to run a single program or forgot to add input, "
        printfn "Usage: dotnet run <program> \nExample: dotnet run \"(+ 1 2 3)\"\n"
        repl()

    // input made by the user
    let input = argv.[0]

    // checks if file being passed in, or direct input on the command line
    if ((input.[(input.Length-4)..(input.Length-1)]) = ".dtm")
    then
        // gets program from file and changes the string from seq<string> to string
        let new_input = readLines input |> Seq.toArray

        // calls the driver method to evaluate the input from file
        driver new_input.[0] new_input.[0]

    else
        // calls the driver method to evaluate the input
        driver argv.[0] input