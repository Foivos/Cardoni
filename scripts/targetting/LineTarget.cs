public class LineTarget: ITarget {
    int Lane { get; set; }

    public LineTarget(int lane) {
        Lane = lane;
    }
}
