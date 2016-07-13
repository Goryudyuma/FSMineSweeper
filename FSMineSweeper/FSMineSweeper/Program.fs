// F# の詳細については、http://fsharp.org を参照してください
// 詳細については、'F# チュートリアル' プロジェクトを参照してください。

open System 
open System.Windows.Forms 
open System.Drawing 




let BombsCount = 10
let MapHigh = 10
let MapWidth = 10
let rand = System.Random()

let firstHigh = 5
let firstWidth = 5

module MakeBombsMap =
    let MapAll = 
        let rec func H W = 
            match H , W with
            | 0, 0 when H = firstHigh && W = firstWidth -> []
            | 0, 0 -> (0, 0) :: []
            | _, 0 when H = firstHigh && W = firstWidth -> func (H - 1) (MapWidth - 1)
            | _, 0 -> (H, W) :: func (H - 1) (MapWidth - 1)
            | _, _ when H = firstHigh && W = firstWidth ->  func H (W - 1)
            | _, _ -> (H, W) :: func H (W - 1)
        func (MapHigh - 1) (MapWidth - 1)
    let BombsMap =
        let ret = MapAll 
        let rec remove lst idx = 
            match lst with
            | [] -> []
            | first :: rest -> 
                if idx = 0 
                then rest
                else first :: remove rest (idx - 1)
        let rec removedList lst count = 
            if count = 0 
            then lst
            else removedList (remove lst (rand.Next (List.length lst))) (count - 1) 
        removedList MapAll BombsCount
        

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // 整数の終了コードを返します