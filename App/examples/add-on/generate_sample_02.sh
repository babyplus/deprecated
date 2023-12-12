grep public ../../models/Mxfile.cs | awk '{if($2=="class"){class=$3}else{print class ","$3",8,"$2}}' | awk -f sample_02.awk > ../source/sample_02.yml
