// parsers constructed to verify input for the DTM language
module ProjectParser

open Parser

(* GRAMMAR OF THE LANGUAGE *)
type Expr =
| Num of float
| Deci of float
| In_Plus of Expr * Expr
| In_Sub of Expr * Expr
| In_Mult of Expr * Expr
| In_Div of Expr * Expr
| In_Expo of Expr * Expr
| Pre_Plus of Expr list
| Pre_Sub of Expr list
| Pre_Mult of Expr list
| Pre_Div of Expr list
| Pre_Expo of Expr list

(* HELPER COMBINATORS *)

// Returns a list of p separated by sep
let pmany2sep p sep = 
    pseq p (pmany1 (pright sep p)) (fun (x,xs) -> x::xs) <!> "pmany2sep"

// Accepts whatever p accepts, surrounded by parens, i.e., (p), and returns whatever p returns
let inParens p = 
    pbetween (pchar '(') (pchar ')') p <!> "inParens"

(* INFIX COMBINATORS *)

// Accepts whatever p and q accepts, surrounded by parens, i.e., (p + q), and returns p and q
let inPlus p q = 
    inParens (pseq (p) (pright (pstr " + ") q) (fun (v1, v2) -> (v1, v2))) <!> "inPlus"

// Accepts whatever p and q accepts, surrounded by parens, i.e., (p - q), and returns p and q
let inSub p q = 
    inParens (pseq (p) (pright (pstr " - ") q) (fun (v1, v2) -> (v1, v2))) <!> "inSub"

// Accepts whatever p and q accepts, surrounded by parens, i.e., (p * q), and returns p and q
let inMult p q = 
    inParens (pseq (p) (pright (pstr " * ") q) (fun (v1, v2) -> (v1, v2))) <!> "inMult"

// Accepts whatever p and q accepts, surrounded by parens, i.e., (p - q), and returns p and q
let inDiv p q = 
    inParens (pseq (p) (pright (pstr " / ") q) (fun (v1, v2) -> (v1, v2))) <!> "inDiv"

// Accepts whatever p and q accepts, surrounded by parens, i.e., (p ^ q), and returns p and q
let inExpo p q = 
    inParens (pseq (p) (pright (pstr " ^ ") q) (fun (v1, v2) -> (v1, v2))) <!> "inPlus"

(* PREFIX COMBINATORS *)

// Accepts whatever p accepts, surrounded by parens, i.e., (+ p), and returns whatever p returns.
let prePlus p = 
    inParens (pright (pstr "+ ") p) <!> "inPlus"

// Accepts whatever p accepts, surrounded by parens, i.e., (- p), and returns whatever p returns.
let preSub p = 
    inParens (pright (pstr "- ") p) <!> "inSub"

// Accepts whatever p accepts, surrounded by parens, i.e., (* p), and returns whatever p returns.
let preMult p = 
    inParens (pright (pstr "* ") p) <!> "inMult"

// Accepts whatever p accepts, surrounded by parens, i.e., (/ p), and returns whatever p returns.
let preDiv p = 
    inParens (pright (pstr "/ ") p) <!> "inDiv"

// Accepts whatever p accepts, surrounded by parens, i.e., (^ p), and returns whatever p returns.
let preExpo p = 
    inParens (pright (pstr "^ ") p) <!> "inExpo"

(* DTM GRAMMAR *)

// allows for the use of expr before specifically defined, recursive definition
let expr, exprImpl = recparser()

/// parser to verify that an input is an integer, ends up being converted into a float
let num = 
    pmany1 pdigit |>> (fun ds -> Num (float (stringify ds))) <!> "num"

/// parser to verify that an input is a negative integer, ends up being converted into a float
let negNum = 
    pseq (pchar '-') (pmany1 pdigit) (fun (sign, ds) -> Num (float (stringify [sign] + stringify ds))) <!> "negNum"

/// parser to verify that an input is a decimal value in the form of a float
let deci = 
    pseq (pmany1 pdigit) (pseq (pchar '.') (pmany1 pdigit) (fun (c1, c2) -> stringify [c1] + stringify c2)) 
         (fun (ds, ps) -> Deci (float ((stringify ds) + ps))) <!> "deci"

/// parser to verify that an input is a negative decimal value in the form of a float
let negDeci = 
    pseq (pchar '-') (pseq (pmany1 pdigit) (pseq (pchar '.') (pmany1 pdigit) (fun (c1, c2) -> stringify [c1] + stringify c2)) 
         (fun (ds, ps) -> ((stringify ds) + ps))) (fun (ds, ps) -> Deci (float ((stringify [ds]) + ps))) <!> "negDeci"

(* INFIX PARSERS *)

/// infix parser to verify that the addition (+) operation is being used correctly
let inPlusExpr = 
    inPlus (expr) (expr) |>> (fun (e1, e2) -> In_Plus(e1, e2)) <!> "inPlusExpr"

/// infix parser to verify that the subtraction (-) operation is being used correctly
let inSubExpr = 
    inSub (expr) (expr) |>> (fun (e1, e2) -> In_Sub(e1, e2)) <!> "inSubExpr"

/// infix parser to verify that the multiplication (*) operation is being used correctly
let inMultExpr = 
    inMult (expr) (expr) |>> (fun (e1, e2) -> In_Mult(e1, e2)) <!> "inMultExpr"

/// infix parser to verify that the division (/) operation is being used correctly
let inDivExpr = 
    inDiv (expr) (expr) |>> (fun (e1, e2) -> In_Div(e1, e2)) <!> "inDivExpr"

/// infix parser to verify that the exponentiation (^) operation is being used correctly
let inExpoExpr = 
    inExpo (expr) (expr) |>> (fun (e1, e2) -> In_Expo(e1, e2)) <!> "inExpoExpr"

(* PREFIX PARSERS *)

/// prefix parser to verify that the addition (+) operation is being used correctly
let prePlusExpr = 
    prePlus (pmany2sep expr pws1) |>> Pre_Plus <!> "prePlusExpr"

/// prefix parser to verify that the substraction (-) operation is being used correctly
let preSubExpr = 
    preSub (pmany2sep expr pws1) |>> Pre_Sub <!> "preSubExpr"

/// prefix parser to verify that the multiplication (*) operation is being used correctly
let preMultExpr = 
    preMult (pmany2sep expr pws1) |>> Pre_Mult <!> "preMultExpr"

/// prefix parser to verify that the division (/) operation is being used correctly
let preDivExpr = 
    preDiv (pmany2sep expr pws1) |>> Pre_Div <!> "preDivExpr"

/// prefix parser to verify that the exponentiation (^) operation is being used correctly
let preExpoExpr = 
    preExpo (pmany2sep expr pws1) |>> Pre_Expo <!> "preExpoExpr"

// cycles through the different parsers available in a valid program, tries to implement PEMDAS
exprImpl := (deci <|> negDeci <|> num <|> negNum) 
             <|> (inExpoExpr <|> inMultExpr <|> inDivExpr <|> inPlusExpr <|> inSubExpr)
             <|> (preExpoExpr <|> preMultExpr <|> preDivExpr <|> prePlusExpr <|> preSubExpr)
             <!> "expr"

/// parser of the input to the program, checks to ensure the end is reached but does not return it
let grammar = pleft expr peof <!> "grammar"