﻿using SwiftMessageReader.Models;

namespace SwiftMessageReader.Data.Interfaces
{
    public interface ISwiftRepository
    {
        void InsertIntoDatabase(MessageModel model);
    }
}