namespace RuichenShuxin.AbpPro;

public static class AbpProDomainErrorCodes
{
    public const string Namespace = "AbpPro";

    public static class Author
    {
        public const string Prefix = Namespace + ":00";
        /// <summary>
        /// 作者 {Name} 已经存在!
        /// </summary>
        public const string AuthorAlreadyExists = Prefix + "001";
    }

}
