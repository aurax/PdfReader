﻿using System;

namespace PdfXenon.Standard
{
    public class PdfReal : PdfObject
    {
        public PdfReal(PdfObject parent, ParseReal real)
            : base(parent, real)
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public ParseReal ParseReal { get => ParseObject as ParseReal; }
        public float Value { get => ParseReal.Value; }
    }
}
