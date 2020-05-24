// an implementation for the parser function parse(s: string) : Expr option

open System
open CS334

// runs the program
[<EntryPoint>]
let main argv =
    if argv.Length <> 1 then
        printfn "Usage: dotnet run <̈program>"
        exit 1
    match parse argv.[0] with
    | Some ast -> printfn "%A" (prettyprint ast)
    | None -> printfn "Invalid program."
    0 // exit code for main