import os
import re
import sys

if len(sys.argv) < 2:
	print("Missing version name")
	sys.exit(1)

new_version = sys.argv[1]
if not re.match(r'\d+\.\d+.\d+', new_version):
    print("Incorrect version pattern, expected: \d+\.\d+.\d+")

current_dir_path = os.path.abspath(os.path.dirname(os.path.basename(__file__)))
files_and_patterns = [
	(os.path.join(current_dir_path, "SailingSkill", "Properties", "AssemblyInfo.cs"), r'Version\("(\d+\.\d+.\d+).\d+"\)', 'Version("%s.0")'),
	(os.path.join(current_dir_path, "SailingSkill", "SailingSkill.cs"), '"SailingSkill", "(\d+\.\d+.\d+)"', '"SailingSkill", "%s"'),
]

for filepath, pattern, replacement_pattern in files_and_patterns:
	with open(filepath, "r") as f:
	    replaced_content = re.sub(pattern, replacement_pattern % new_version, f.read())
	with open(filepath, "w") as f:
	    f.write(replaced_content)
