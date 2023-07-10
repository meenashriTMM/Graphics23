namespace GrayBMP;

#region class PolyFill -----------------------------------------------------------------------------
class PolyFill {
   #region Methods ---------------------------------------------------
   public void AddLine (int x1, int y1, int x2, int y2) {
      if (y1 == y2) return;
      if (y1 > y2) (x1, y1, x2, y2) = (x2, y2, x1, y1);
      double dy = y2 - y1, dx = x2 - x1;
      mLines.Add ((x1, y1, x2, y2, dx / dy));
   }

   public void Fill (GrayBMP bmp, int gray) {
      var xInters = new List<int> ();
      for (int i = 0; i < bmp.Height; i++) {
         xInters.Clear ();
         for (int a = 0; a < mLines.Count; a++)
            if (GetXIntersection (a, i + 0.5, out double res)) xInters.Add ((int)res);
         xInters = xInters.Order ().ToList ();
         for (int j = 0; j < xInters.Count; j += 2) bmp.DrawHorizontalLine (xInters[j], xInters[j + 1], bmp.Height - i, gray);
      }
   }
   #endregion

   /// <summary> Returns the X intersection of the horizontal line segment and any line segment </summary>
   bool GetXIntersection (int lineIdx, double yInter, out double xInter) {
      xInter = double.NaN;
      var line = mLines[lineIdx];
      int x1 = line.X1, y1 = line.Y1, x2 = line.X2, y2 = line.Y2;
      if (yInter < y1 || yInter > y2) return false;
      xInter = x1 == x2 ? x1 : x1 + (yInter - y1) * line.InverseSlope;
      return true;
   }

   #region Private data ---------------------------------------------
   List<(int X1, int Y1, int X2, int Y2, double InverseSlope)> mLines = new ();
   #endregion
}
#endregion
