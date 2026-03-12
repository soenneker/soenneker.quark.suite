namespace Soenneker.Quark;

/// <summary>
/// Contains literal Tailwind class names so the CLI content scanner detects them.
/// Margin, Padding, and BorderRadius builders construct these at runtime from parts.
/// </summary>
internal static class TailwindContentStub
{
    // Margin: m + sides(t,e,b,s,x,y) + sizes(0,1,2,3,4,5,auto)
    private const string Margin = "m-0 m-1 m-2 m-3 m-4 m-5 m-auto mt-0 mt-1 mt-2 mt-3 mt-4 mt-5 mt-auto mb-0 mb-1 mb-2 mb-3 mb-4 mb-5 mb-auto ms-0 ms-1 ms-2 ms-3 ms-4 ms-5 ms-auto me-0 me-1 me-2 me-3 me-4 me-5 me-auto mx-0 mx-1 mx-2 mx-3 mx-4 mx-5 mx-auto my-0 my-1 my-2 my-3 my-4 my-5 my-auto";

    // Padding: p + sides(t,e,b,s,x,y) + sizes(0,1,2,3,4,5)
    private const string Padding = "p-0 p-1 p-2 p-3 p-4 p-5 pt-0 pt-1 pt-2 pt-3 pt-4 pt-5 pb-0 pb-1 pb-2 pb-3 pb-4 pb-5 ps-0 ps-1 ps-2 ps-3 ps-4 ps-5 pe-0 pe-1 pe-2 pe-3 pe-4 pe-5 px-0 px-1 px-2 px-3 px-4 px-5 py-0 py-1 py-2 py-3 py-4 py-5";

    // BorderRadius: rounded + corners(tl,tr,bl,br,t,b,l,r) + sizes(0,1,2,3,4,5,pill,circle,sm,md,lg)
    private const string BorderRadius = "rounded rounded-0 rounded-1 rounded-2 rounded-3 rounded-4 rounded-5 rounded-sm rounded-md rounded-lg rounded-pill rounded-circle rounded-t rounded-b rounded-l rounded-r rounded-tl rounded-tr rounded-bl rounded-br rounded-s-none rounded-b-none";
}
