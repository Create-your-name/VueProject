﻿namespace webapi.Bean
{
    public class Depart
    {
        public string? no { get; set; }
        public string? ABN { get; set; }
        public string? location { get; set; }
        public string? c1 { get; set; }
        public string? c2 { get; set; }
        public string? c3 { get; set; }
        public string? c4 { get; set; }
        public string? YY6 { get; set; }
        public string? ycclBm { get; set; }
        public string? z1 { get; set; }
        public string? z2 { get; set; }
        public string? z3 { get; set; }
        public string? z4 { get; set; }
        public string? YY62 { get; set; }
        public string? BZ { get; set; }

        public Depart(string no, string v1, string? location, string? c1, string? c2, string? c3, string? c4, string YY6, string? ycclBm, string? z1, string? z2, string? z3, string? z4, string YY62, string BZ)
        {
            this.no = no;
            this.ABN = v1;
            this.location = location;
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
            this.c4 = c4;
            this.YY6 = YY6;
            this.ycclBm = ycclBm;
            this.z1 = z1;
            this.z2 = z2;
            this.z3 = z3;
            this.z4 = z4;
            this.YY62 = YY62;
            this.BZ = BZ;
        }
    }
}
