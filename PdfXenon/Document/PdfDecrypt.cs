﻿using System;

namespace PdfXenon.Standard
{
    public abstract class PdfDecrypt : PdfObject
    {
        public PdfDecrypt(PdfDocument doc)
            : base(doc)
        {
        }

        public static PdfDecrypt CreateDecrypt(PdfDocument doc, ParseDictionary trailer)
        {
            PdfDecrypt ret = new PdfDecryptNone(doc);

            // Check for optional encryption reference
            ParseObjectReference encryptRef = trailer.OptionalValue<ParseObjectReference>("Encrypt");
            if (encryptRef != null)
            {
                ParseDictionary encryptDict = doc.IndirectObjects.OptionalValue<ParseDictionary>(encryptRef);
                ParseName filter = encryptDict.MandatoryValue<ParseName>("Filter");
                ParseInteger v = encryptDict.OptionalValue<ParseInteger>("V");

                // We only implement the simple Standard, Version 1 scheme
                if ((filter.Value == "Standard") && (v != null) && (v.Value == 1))
                {
                    // Extract the first document identifier
                    ParseArray ids = trailer.MandatoryValue<ParseArray>("ID");
                    ParseString id0 = (ParseString)ids.Objects[0];
                    byte[] documentId = id0.ValueAsBytes;

                    // 
                    ret = new PdfDecryptStandard(doc, encryptDict, documentId);
                }

                throw new ApplicationException("Can only decrypt the standard handler with version 1.");
            }

            return ret;
        }
    }
}