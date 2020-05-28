// interpreter used to evaluate input for the DTM language
module ProjectInterpreter

open System.IO
open Parser
open ProjectParser

/// reads the lines of a file
// method from ChaosPandion on StackOverflow, source: https://stackoverflow.com/questions/2365527/how-read-a-file-into-a-seq-of-lines-in-f
let readLines (filePath:string) = seq {
    use sr = new StreamReader (filePath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}

/// evaluates a program based on what type of expr it matches
let rec eval e =
    match e with
    | Num n    -> n
    | Deci d    -> d

    // infix operations, asks for arguments in the form of ( (A * B) + (C / D) )
    | In_Plus (p1, p2)   -> (eval p1) + (eval p2)
    | In_Sub (s1, s2)    -> (eval s1) - (eval s2)
    | In_Mult (m1, m2)   -> (eval m1) * (eval m2)
    | In_Div (d1, d2)    -> if (eval d2 = 0.0) 
                            then 
                                printfn "ERROR: Cannot divide by 0" 
                                exit 1 
                            else 
                                (eval d1) / (eval d2)                           
    | In_Expo (e1, e2)   -> (eval e1) ** (eval e2)

    // prefix operations, asks for arguments in the form of (+ (* A B) (/ C D) )
    | Pre_Plus p    -> p  |> List.map eval |> List.sum
    | Pre_Sub s     -> s  |> List.map eval |> List.reduce (fun acc i -> acc - i)
    | Pre_Mult m    -> m  |> List.map eval |> List.fold (fun acc i -> acc * i) 1.0 
    | Pre_Div di    -> di |> List.map eval |> List.reduce (fun acc i -> acc / i)
    | Pre_Expo ex   -> ex |> List.map eval |> List.reduce (fun acc i -> acc ** i)

/// driver program for the language, parses and evaluates input passed in
let driver arg input = 
    match grammar (prepare input) with
    | Success(res,_) ->
        printfn "%A" (eval res)
        0
    | Failure(pos, rule) ->
        // tells user why their program failed and where the parser was unable to read input
        printfn "Invalid program."
        let msg = sprintf "Cannot parse input at pos %d in rule '%s':" pos rule
        let diag = diagnosticMessage 50 pos arg msg
        printf "%s" diag
        1