namespace GrayBMP;

#region class PolyFill -----------------------------------------------------------------------------
class PolyFill {
   #region Methods ---------------------------------------------------
   public void AddLine (int x1, int y1, int x2, int y2) => mLines.Add ((x1, y1, x2, y2));

   public void Fill (GrayBMP bmp) {
      for (int i = 0; i < bmp.Height; i++) {
         var xInters = new List<int> ();
         foreach (var line in mLines)
            if (GetXIntersection (line.X1, line.Y1, line.X2, line.Y2, i + 0.5, out double res)) xInters.Add ((int)res);
         xInters = xInters.Order ().ToList ();
         for (int j = 0; j < xInters.Count; j += 2) bmp.DrawHorizontalLine (xInters[j], xInters[j + 1], i, 255);
      }
   }
   #endregion

   /// <summary> Returns the X intersection of the horizontal line segment and any line segment </summary>
   bool GetXIntersection (int x1, int y1, int x2, int y2, double yInter, out double xInter) {
      xInter = double.NaN;
      if (y1 > y2) (x1, y1, x2, y2) = (x2, y2, x1, y1);
      double dy = y2 - y1, dx = x2 - x1;
      if (dy == 0 || yInter < y1 || yInter > y2) return false;
      xInter = dx == 0 ? x1 : x1 + dx * (yInter - y1) / dy;
      return true;
   }

   #region Private data ---------------------------------------------
   List<(int X1, int Y1, int X2, int Y2)> mLines = new ();
   #endregion
}
#endregion
