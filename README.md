# DTM Language


## What is DTM?
*Excerpt from the [Tutorial sheet](./tutorial/tutorial.pdf)*

"As a language, Do The Math (DTM) serves as a tool to solve math problems. 
Given the way expressions are formatted, DTM reinforces the order of operation 
rules and avoids problems that are open to interpretation in how they should be 
solved. This is done with the use of parentheses around all possible operations 
between two numbers. The language is written in the F# programming language 
and is built using the .NET Framework from Microsoft. Although it was written 
intended to take simple expressions from the command line, DTM also supports 
running files with an extension of ".dtm" , and has a functioning repl that can be 
used in the command line. The language implements the evaluation of math problems 
in prefix and infix notation. For more information giving a much more detailed 
description of the language, its syntax, and its semantics, be sure to read 
[*A Look into DTM (Do the Math)*](./spec/lang-spec.pdf), which is included in the 
language’s source code."

## Getting Started

In order to use DTM:
- Clone this repository to your local machine using  `git clone https://github.com/japonte21/dtm-language.git`
- Run the program with these commands
```
$ cd dtm-language
$ dotnet build
...
$ dotnet run --project lang
```
or
```
$ cd dtm-language/lang
$ dotnet build
...
$ dotnet run
```

This will allow you to have a copy of the project on your local machine available to run when desired.

### Prerequisites

Ensure that you have a version of F# and .NET compatible with your local machine. F# is needed to 
make any desired edits to the source code and .NET is used to build and run the language. To check 
this, run the command `dotnet --version` and `fsharpi` in terminal. If you don't have either of the 
programs, download the latest version compatible with your machine on the 
[.NET website](https://dotnet.microsoft.com/download) and the [F# website](https://fsharp.org/) .
