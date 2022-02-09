from ast import parse
from os import fdopen
from tokenize import String
import uuid
import base64

my_guid = uuid.uuid4()
# print(my_guid)

print(base64.b64encode(my_guid.bytes))

my_hash = str(base64.b64encode(my_guid.bytes))
print(my_hash[2:-3])

f_handle = open("../my_file","w")
f_handle.write(my_hash[2:-3])
f_handle.close()