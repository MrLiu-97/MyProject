// Decompiled with JetBrains decompiler
// Type: dangdangSDK.IClient
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

namespace dangdangSDK
{
    public interface IClient
    {
        T Execute<T>(IRequest<T> request) where T : DDResponse, new();
    }
}
