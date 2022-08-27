import os
import sys
from pathlib import Path
from uuid import uuid4


mode = input().strip()
did = b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x91\x03\x89\x72'
pid = b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x13\x03\xc1\x42'
sid = b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x9b\xe9\x19\xaa'
ids = { 'Workshop': pid, 'Staging': sid, 'Dev': did }
wid = ids[mode]


def fix(fn: str):
    with open(fn, 'rb') as f:
        b = f.read()
    for qid in ids.values():
        b = b.replace(qid, wid)
    with open(fn, 'wb') as f:
        f.write(b)


for path in Path('content/TMG Levels').glob('*.lev'):
    fix(path)
print('done')
