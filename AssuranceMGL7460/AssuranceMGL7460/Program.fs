// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.


// Useful linkf for us Alex:
// http://fsharp.github.io/FSharp.Data/library/JsonProvider.html
// https://msdn.microsoft.com/visualfsharpdocs/conceptual/visual-fsharp


open FSharp.Data;
open FSharp.Linq;



// just an examples
type Simple = JsonProvider<""" { "name":"John", "age":94 } """>
let simple = Simple.Parse(""" { "name":"Tomas", "age":4 } """)



[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
