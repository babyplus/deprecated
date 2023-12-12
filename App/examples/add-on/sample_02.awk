function rand_pro(){
  rand()
  rand()
  return rand()rand()
}

function print_head()
{
    print "diagrams:"
    print "- mxGraphModels:"
    print "  - roots:"
    print "    - mxcells:"
    print "      - id: 0"
}

function print_parent(ps, p)
{
    print "      - id: "ps[p]
    print "        parent: 0"
}

function print_bus(buffs, vs, p, v, g)
{
    buffs[length(buffs)+1]=sprintf("      - id: bus_%s", v)
    buffs[length(buffs)+1]=sprintf("        parent: %s", p)
    buffs[length(buffs)+1]=sprintf("        value: \"&nbsp; &nbsp;- %s  %s\"", types[v], descs[v])
    buffs[length(buffs)+1]=sprintf("        style: \"html=1;outlineConnect=0;fillColor=#88E5C5;strokeColor=#288565;gradientDirection=north;strokeWidth=2;shape=mxgraph.networks.bus;gradientDirection=north;fontColor=#222222;perimeter=backbonePerimeter;backboneSize=20;shadow=0;sketch=0;opacity=100;fontSize=18;align=left\"")
    buffs[length(buffs)+1]=sprintf("        vertex: 1")
    buffs[length(buffs)+1]=sprintf("        mxGeometry:")
    buffs[length(buffs)+1]=sprintf("          x: 100")
    buffs[length(buffs)+1]=sprintf("          y: %s", g["y"])
    buffs[length(buffs)+1]=sprintf("          width: 1400")
    buffs[length(buffs)+1]=sprintf("          height: 50")
    buffs[length(buffs)+1]=sprintf("          as: geometry")
    g["y"]+=50
}

function print_block(buffs, vs, p, v, g)
{
    buffs[length(buffs)+1]=sprintf("      - id: blk_%s", v)
    buffs[length(buffs)+1]=sprintf("        parent: %s", p)
    buffs[length(buffs)+1]=sprintf("        value: \"%s\"", maps[v])
    buffs[length(buffs)+1]=sprintf("        style: \"rounded=0;whiteSpace=wrap;html=1;\"")
    buffs[length(buffs)+1]=sprintf("        vertex: 1")
    buffs[length(buffs)+1]=sprintf("        mxGeometry:")
    buffs[length(buffs)+1]=sprintf("          x: %s", g["x"])
    buffs[length(buffs)+1]=sprintf("          y: %s", g["y"])
    buffs[length(buffs)+1]=sprintf("          width: 150")
    buffs[length(buffs)+1]=sprintf("          height: 50")
    buffs[length(buffs)+1]=sprintf("          as: geometry")
    g["x"]+=150
}

function print_line(buffs, p, v)
{
    buffs[length(buffs)+1]=sprintf("      - id: line_%s", v)
    buffs[length(buffs)+1]=sprintf("        parent: %s", p)
    buffs[length(buffs)+1]=sprintf("        edge: 1")
    buffs[length(buffs)+1]=sprintf("        style: \"endArrow=none;html=1;rounded=0;entryX=0.8;entryY=1;entryDx=0;entryDy=0;strokeColor=#288565;\"")
    buffs[length(buffs)+1]=sprintf("        source: bus_%s", v)
    buffs[length(buffs)+1]=sprintf("        target: blk_%s", v)
    buffs[length(buffs)+1]=sprintf("        mxGeometry:")
    buffs[length(buffs)+1]=sprintf("          as: geometry")
    buffs[length(buffs)+1]=sprintf("          relative: 1")
    buffs[length(buffs)+1]=sprintf("          mxPoint:")
    buffs[length(buffs)+1]=sprintf("          - as: sourcePoint")
    buffs[length(buffs)+1]=sprintf("          - as: targetPoint")
}

function print_background(ps, p, g1, g2)
{
    print "      - id: bg_"p
    print "        parent: "ps[p]
    print "        style: \"rounded=0;whiteSpace=wrap;html=1;\""
    print "        vertex: 1"
    print "        mxGeometry:"
    print "          x: "g1["x"]
    print "          y: "g1["y"]
    print "          width: 1500"
    print "          height: "g2["y"]-g1["y"]
    print "          as: geometry"
    g["x"]+=120
}

{
    rows[NR]=$0
}

END{
    srand(systime())
    print_head()
    geo["y"]=100
    fields[""]=""
    maps[""]=""
    types[""]=""
    for(i=1;i<=length(rows);i++)
    {
        split(rows[i],tmp_columns,",")
        {
            parent=tmp_columns[1]
            field=tmp_columns[2]
            nlen=tmp_columns[3]
            type=tmp_columns[4]
            desc=tmp_columns[5]
            if(!parents[parent])parents[parent]=rand_pro()
            fields[parent"."field]=rand_pro()
            types[fields[parent"."field]]=type
            maps[fields[parent"."field]]=parent"."field
            descs[fields[parent"."field]]="."
        }
    }
    for(i in parents)
    {
        bg_geo["x"]=50
        bg_geo["y"]=geo["y"]
        geo["x"]=150
        geo["y"]+=100
        buffs[0]=""
        print_parent(parents, i)
        for(j in fields)
        {
            if(latest!=parent)geo["y"]+=50
            split(j,A,".")
            parent=A[1]
            field=A[2]
            if(parent==i)print_block(buffs, fields, parents[parent], fields[j], geo)
            latest=parent
        }
        geo["y"]+=100
        for(j in fields)
        {
            split(j,A,".")
            parent=A[1]
            field=A[2]
            if(parent==i)print_bus(buffs, fields, parents[parent], fields[j], geo)
            if(parent==i)print_line(buffs, parents[parent], fields[j])
        }
        geo["y"]+=50
        print_background(parents, i, bg_geo, geo)
        for (b in buffs) if (buffs[b]) print buffs[b]
        delete buffs
    }
}
