import sys

def getSortedLetters (string):
	return ''.join(sorted(list(string)))

wordsByLength = {}

with open('wordsEn.txt', 'r') as f:
	for line in f:
		line = line.rstrip()
		length = len(line)
		if length > 3 and length < 19:
			if length not in wordsByLength:
				wordsByLength[length] = {}
			wordsByLetters = wordsByLength[length]
			sortedLetters = getSortedLetters(line)
			if sortedLetters not in wordsByLetters:
				wordsByLetters[sortedLetters] = []
			wordsByLetters[sortedLetters].append(line)

with open('output.txt', 'wb') as f:
	for wordLength in sorted(wordsByLength):
		for sortedLetters in sorted(wordsByLength[wordLength]):
			for word in wordsByLength[wordLength][sortedLetters]:
				f.write(word)
				f.write('\n')
			f.write('\n')