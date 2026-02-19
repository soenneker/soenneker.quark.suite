namespace Soenneker.Quark;

public static class Space
{
    public static SpaceBuilder XReverse => new("space-x-reverse");
    public static SpaceBuilder YReverse => new("space-y-reverse");

    public static class X
    {
        public static SpaceBuilder Is0 => new("space-x-0");
        public static SpaceBuilder Is1 => new("space-x-1");
        public static SpaceBuilder Is2 => new("space-x-2");
        public static SpaceBuilder Is3 => new("space-x-3");
        public static SpaceBuilder Is4 => new("space-x-4");
        public static SpaceBuilder Is5 => new("space-x-5");
        public static SpaceBuilder Is6 => new("space-x-6");
        public static SpaceBuilder Is7 => new("space-x-7");
        public static SpaceBuilder Is8 => new("space-x-8");
    }

    public static class Y
    {
        public static SpaceBuilder Is0 => new("space-y-0");
        public static SpaceBuilder Is1 => new("space-y-1");
        public static SpaceBuilder Is2 => new("space-y-2");
        public static SpaceBuilder Is3 => new("space-y-3");
        public static SpaceBuilder Is4 => new("space-y-4");
        public static SpaceBuilder Is5 => new("space-y-5");
        public static SpaceBuilder Is6 => new("space-y-6");
        public static SpaceBuilder Is7 => new("space-y-7");
        public static SpaceBuilder Is8 => new("space-y-8");
    }
}
