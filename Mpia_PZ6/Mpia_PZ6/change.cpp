#include "change.h"
#include <algorithm>


std::vector<int> get_change(const std::vector<int>& denominations, int amount) 
{
	std::vector <double> koef;
	std::vector<int> answer;
	auto vs = denominations;

	std::sort(vs.begin(), vs.end());
	if (amount == 0)
		return {};
	int N = vs.size() - 1, summ = 0;
	if (N == 0)
	{
		while (summ != amount)
		{
			answer.push_back(denominations[N]);
			summ += denominations[N];
		}
		return answer;
	}

	for (int i = N; i > 0; i--)
	{
		if (vs[i] == amount)
		{
			answer.push_back(vs[i]);
			return answer;
		}
		
	}

	
	return answer;
}
