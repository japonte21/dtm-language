// interpreter used to evaluate input for the DTM language
module ProjectInterpreter

open ProjectParser

/// evaluates a program based on what type of expr it matches
let rec eval e =
    match e with
    | Deci d    -> d

    // eager evaluations of the values, evaluate arguments first and then does the operations
    | Plus p    -> p |> List.map eval |> List.sum
    | Sub s     -> s |> List.map eval |> List.reduce (fun acc i -> acc - i)
    | Mult m    -> m |> List.map eval |> List.fold (fun acc i -> acc * i) 1.0 
    | Div di    -> di |> List.map eval |> List.reduce (fun acc i -> acc / i)
    | Expo ex   -> ex |> List.map eval |> List.reduce (fun acc i -> acc ** i)
