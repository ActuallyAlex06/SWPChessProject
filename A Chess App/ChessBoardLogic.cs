
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Chess_App
{
    public static class ChessBoardLogic
    { 
    //{
    //    static List<string> coord1 = new List<string> { "A", "B", "D", "E", "F", "G", "H" };

    //    public static int BlackPawnMath(int pos)
    //    {
    //        return pos - 1;
    //    }

    //    public static int WhitePawnMath(int pos)
    //    {
    //        return pos + 1;
    //    }

    //    public List<string> FigureOutPossibleMoves(string piece, string coordinates)
    //    {
    //        List<string> moves = new List<string> { };
    //        specifics = coordinates.Split(" ");

    //        if (piece.Equals("pawnb.png"))
    //        {
    //            if (specifics[1].Equals("7"))
    //            {
    //                moves.Add(specifics[0] + "5");
    //            }

    //            if (CheckButtonPositions(specifics, piece))
    //            {
    //                moves.Add(specifics[0] + ChessBoardLogic.BlackPawnMath(int.Parse(specifics[1])));
    //            }

    //        }
    //        else if (piece.Equals("pawnw.png"))
    //        {

    //            if (specifics[1].Equals("2") && A3.ImageSource == null || specifics[1].Equals("2") && A4.ImageSource == null)
    //            {
    //                moves.Add(specifics[0] + "4");
    //            }

    //            moves.Add(specifics[0] + ChessBoardLogic.WhitePawnMath(int.Parse(specifics[1])));

    //        }

    //        return moves;
    //    }


    //    private bool CheckButtonPositions(string[] coordinates, string piece)
    //    {
    //        List<Button> lstbuttons = GetEachButton();
    //        switch (piece)
    //        {
    //            case "pawnb.png":
    //                foreach (Button button in lstbuttons)
    //                {
    //                    if (button.Text.Equals(specifics[0] + " " + ChessBoardLogic.BlackPawnMath(int.Parse(specifics[1]))))
    //                    {
    //                        if (button.ImageSource != null)
    //                        {
    //                            return false;
    //                        }
    //                    }
    //                    else { return true; }
    //                }
    //                break;
    //        }
    //        return true;
    //    }


    //    private List<Button> GetEachButton()
    //    {
    //        {
    //            List<Button> li = new List<Button>();
    //            foreach (var x in board)
    //            {
    //                if (x is Button)
    //                {
    //                    li.Add((Button)x);
    //                }
    //            }
    //            return li;
    //        }
    //    }
    }
}
