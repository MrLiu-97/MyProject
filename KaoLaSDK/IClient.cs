﻿// Decompiled with JetBrains decompiler
// Type: kaolaSDK.IClient
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

namespace kaolaSDK
{
    public interface IClient
    {
        T Execute<T>(IRequest<T> request) where T : KLResponse, new();
    }
}
