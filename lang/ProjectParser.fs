// parsers constructed to verify  input for the DTM language
module ProjectParser

open Parser

(* grammar of the language *)
type Expr =
| Deci of float
| Plus of Expr list
| Sub of Expr List
| Mult of Expr List
| Div of Expr List
| Expo of Expr List

(* helper combinators *)

// Returns a list of p separated by sep
let pmany2sep p sep = pseq p (pmany1 (pright sep p)) (fun (x,xs) -> x::xs) <!> "pmany2sep"

// Accepts whatever p accepts, surrounded by parens, i.e., (p), and returns whatever p returns
let inParens p = pbetween (pchar '(') (pchar ')') p <!> "inParens"

// helper that accepts whatever p accepts, surrounded by parens, i.e., (+ p), and returns whatever p returns.
let inPlus p = inParens (pright (pstr "+ ") p) <!> "inPlus"

// helper that accepts whatever p accepts, surrounded by parens, i.e., (- p), and returns whatever p returns.
let inSub p = inParens (pright (pstr "- ") p) <!> "inSub"

// helper that accepts whatever p accepts, surrounded by parens, i.e., (* p), and returns whatever p returns.
let inMult p = inParens (pright (pstr "* ") p) <!> "inMult"

// helper that accepts whatever p accepts, surrounded by parens, i.e., (/ p), and returns whatever p returns.
let inDiv p = inParens (pright (pstr "/ ") p) <!> "inDiv"

// helper that accepts whatever p accepts, surrounded by parens, i.e., (^ p), and returns whatever p returns.
let inExpo p = inParens (pright (pstr "^ ") p) <!> "inExpo"

(* DTM grammar *)

// allows for the use of expr before specifically defined, recursive definition
let expr, exprImpl = recparser()

/// parser to verify that an input is a number in the form of a float or an integer
let deci = pmany1 pdigit |>> (fun ds -> Deci (float (stringify ds))) <!> "deci"

/// parser to verify that the addition (+) operation is being used correctly
let plusExpr =inPlus (pmany2sep expr pws1) |>> (fun es -> Plus(es)) <!> "plusExpr"

/// parser to verify that the substraction (-) operation is being used correctly
let subExpr = inSub (pmany2sep expr pws1) |>> (fun es -> Sub(es)) <!> "SubExpr"

/// parser to verify that the multiplication (*) operation is being used correctly
let multExpr = inMult (pmany2sep expr pws1) |>> (fun es -> Mult(es)) <!> "multExpr"

/// parser to verify that the division (/) operation is being used correctly
let divExpr = inDiv (pmany2sep expr pws1) |>> (fun es -> Div(es)) <!> "divExpr"

/// parser to verify that the exponentiation (^) operation is being used correctly
let expoExpr = inExpo (pmany2sep expr pws1) |>> (fun es -> Expo(es)) <!> "expoExpr"

// cycles through the different parsers available in a valid program, tries to implement PEMDAS
exprImpl := expoExpr <|> multExpr <|> divExpr <|> plusExpr <|> subExpr <|> deci <!> "expr"

/// parser of the input to the program, checks to ensure the end is reached but does not return it
let grammar = pleft expr peof <!> "grammar"