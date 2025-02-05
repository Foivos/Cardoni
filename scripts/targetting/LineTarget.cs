namespace Cardoni;

public class LineTarget: CardTarget {
    int Lane { get; set; }

    public LineTarget(int lane) {
        Lane = lane;
    }
}
