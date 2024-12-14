public class FaceResult
{
    public string FaceId { get; set; }
    public FaceRectangle FaceRectangle { get; set; }
    public FaceAttributes FaceAttributes { get; set; }
}

public class FaceRectangle
{
    public int Top { get; set; }
    public int Left { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class FaceAttributes
{
    public double Age { get; set; }
    public string Gender { get; set; }
    public string Smile { get; set; }
    public FaceHair Hair { get; set; }
}

public class FaceHair
{
    public string HairColor { get; set; }
    public string HairType { get; set; }
}
