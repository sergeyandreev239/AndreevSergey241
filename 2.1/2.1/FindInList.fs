﻿
module logic

  //  let li = [1;2;3;4;5]
   // let N = System.Console.ReadLine() |> int 
    
    let check list1 template =
        let rec ch list1 template count =
            let compare a b =
                if a=b then true
                else false
            match list1 with
            | x::xs -> compare x template ; if not (compare x template) then ch xs template (count + 1) else Some(count)
            | [] -> None
        ch list1 template 1
   // printf "%A" (check li N )
   // System.Console.ReadKey();