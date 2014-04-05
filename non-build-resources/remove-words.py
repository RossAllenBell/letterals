import sys

badWords = []
badWordParts = []

with open('bad-words.txt', 'r') as f:
	for line in f:
		line = line.rstrip()
		length = len(line)
		if length > 0 and line[0] != '#':
			badWords.append(line)
			badWords.append(line + 's')
			badWords.append(line + 'es')

with open('bad-word-parts.txt', 'r') as f:
	for line in f:
		line = line.rstrip()
		length = len(line)
		if length > 0 and line[0] != '#':
			badWordParts.append(line)

with open('wordsEn.txt', 'r') as f:
	with open('wordsEn-filtered.txt', 'wb') as w:
		for line in f:
			bad = False
			for badWord in badWords:
				stripped = line.rstrip()
				if stripped == badWord or stripped == badWord:
					bad = True
					print line
					break
			if not bad:
				for badWordPart in badWordParts:
					if line.find(badWordPart) != -1:
						bad = True
						print line
						break
			if not bad:
				w.write(line)