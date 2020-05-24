module CS334

open System
open Parser

// grammar given
type Expr =
| Variable of char
| Abstraction of char * Expr
| Application of Expr * Expr

/// allows for use of expr before defining it completely
let expr, exprImpl = recparser()

// helper methods for the structure of the parsers
let absOpener1 = (pstr "(L")
let absOpener2 = (pstr "(λ")
let period = (pchar '.')
let openParen = (pchar '(')
let closedParen = (pchar ')')

/// checks if a value fits the definition of a variable
let variable : Parser<Expr> = pletter |>> (fun v -> Variable v)

/// checks the internal part of an abstraction to ensure the first variable and period are there
let absInternal1 = pleft (pletter) (period)

/// checks the internal part of an abstraction to ensure the previous values are there along with a valid expression
let absInternal2 = pseq (absInternal1) (expr) (fun (a, b) -> Abstraction (a, b))

/// checks if an expression fits the form of an abstraction, accepts L or λ
let abstraction : Parser<Expr> = (pbetween (absOpener1) (closedParen) (absInternal2)) 
                              <|> pbetween (absOpener2) (closedParen) (absInternal2) 

/// checks the internal parts of an application expression to ensure it fits appropriate form
let appInternal = pseq (expr) (expr) (fun (a,b) -> (a,b))

/// checks if an expression fits the form of an application
let application : Parser<Expr> = pbetween (openParen) (closedParen) (appInternal) |>> (fun e -> Application e)

/// cycles through the different parsers available in a valid program
exprImpl := (variable <|> abstraction <|> application)

/// parser of the input to the program, checks to ensure the end is reached but does not return it
let grammar = pleft expr peof

/// actual parser for input, prepares texts as input instead of string for grammar
let parse(s: string) : Expr option = 
    match grammar (prepare s) with 
    | Success(x,_) -> Some x
    | Failure (_,_) -> None

/// returns parsed program into a readable string for user
let rec prettyprint(e: Expr) : string =
  match e with
    | Variable var -> "Variable(" + string(var) + ")"
    | Abstraction (var, exp) -> "Abstraction(" + "Variable(" + string(var) + "), " + prettyprint exp + ")"
    | Application (exp1, exp2) -> "Application(" + prettyprint exp1 + ", " + prettyprint exp2 + ")"